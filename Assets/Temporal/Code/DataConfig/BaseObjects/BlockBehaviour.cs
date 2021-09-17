using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
namespace Code.DataConfig.BaseObjects
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BlockBehaviour
    {
        [ColorBox, DisableInEditorMode]
        public string target;
        [ColorBox, DisableInEditorMode]
        public string field;
        [ColorBox, DisableInEditorMode]
        public AnimatorConditionMode condition;
        [ColorBox, DisableInEditorMode]
        public int value;
        [ColorBox, DisableInEditorMode]
        public string state;

        public static BlockBehaviour AddBehaviourToList(string _target, string _field, AnimatorConditionMode _condition, int _value, string _state, BaseBlock _block)
        {
            var decision = new BlockBehaviour{
                target = _target, field = _field, condition = _condition, value = _value,
                state = _state
            };

            _block.behaviourTable.Add(decision);

            var fsm = _block.behaviourData.layers[0].stateMachine;
            if (_block.animationList != null)
            {
                var animList = _block.animationList;
                fsm.states = null;
                _block.behaviourData.parameters = null;
                var transitions = fsm.GetStateMachineTransitions(fsm);
                transitions = null;

                var t = fsm.AddState("CheckAction", new Vector3(-100, 100*animList.Count/2, 0));

                //Create States
                for (var i = 0; i < animList.Count; i++)
                {
                    //Add a state named clip.name, whose position is (250,100,0)
                    var state = fsm.AddState(animList[i].name, new Vector3(250, 100*i, 0));
                    fsm.entryPosition = new Vector3(600, 100*i/2, 0);

                    //The animation of this state is clip
                    state.motion = animList[i];



                    //Set this state to the default state
                    if (animList[i].name == "Idle")
                    {
                        fsm.anyStatePosition = new Vector3(600, 100*i, 0);

                        _block.behaviourData.AddParameter("Action", AnimatorControllerParameterType.Trigger);
                        _block.behaviourData.AddParameter("TurnFinished", AnimatorControllerParameterType.Trigger);

                        var fromAnyState = fsm.AddAnyStateTransition(state);
                        fromAnyState.AddCondition(AnimatorConditionMode.Equals, 0, "TurnFinished");

                        fsm.defaultState = state;


                        var transition = state.AddTransition(t);
                        transition.AddCondition(AnimatorConditionMode.Equals, 0, "Action");
                    }
                }
                var tempList = new List<string>();
                //Create Parameters from List
                foreach (var param in _block.behaviourTable.Select(item => item.target + item.field).Where(param => !tempList.Contains(param)))
                {
                    _block.behaviourData.AddParameter(param, AnimatorControllerParameterType.Int);
                    tempList.Add(param);
                }

                //Set transitions
                if (fsm.states != null)
                    for (var i = 0; i < fsm.states.Length; i++)
                    {
                        var state = fsm.states[i].state;

                        //Add transition status
                        foreach (var item in _block.behaviourTable)
                        {
                            if (item.state == state.name)
                            {
                                var transition = t.AddTransition(state);
                                transition.AddCondition(item.condition, item.value, item.target + item.field);
                            }
                        }
                    }
            }


            var data = _block.behaviourTable;

            return decision;
        }
    }
}