using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
namespace Code.DataConfig.BaseObjects
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BlockBehaviour
    {
        [ColorBox, ValueDropdown("TargetsValues"), DisableInEditorMode]
        public string target;
        [ColorBox, ValueDropdown("FieldsValues"), DisableInEditorMode]
        public string field;
        [ColorBox, DisableInEditorMode]
        public AnimatorConditionMode condition;
        [ColorBox, DisableInEditorMode]
        public int value;
        [ColorBox, DisableInEditorMode]
        public string parameter;

        private IEnumerable ConditionValues;

        private IEnumerable TargetsValues;

        private IEnumerable FieldsValues;
        public BlockBehaviour()
        {
            FieldsValues = new ValueDropdownList<string>(){
                {
                    "Health"
                },{
                    "Distance"
                },
            };

            TargetsValues = new ValueDropdownList<string>(){
                {
                    "This block"
                },{
                    "Character"
                },
            };
        }

        public static BlockBehaviour AddBehaviourToList(string _target, string _field, AnimatorConditionMode _condition, int _value, string _parameter, BaseBlock _block)
        {
            var decision = new BlockBehaviour
            {
                target = _target,
                field = _field,
                condition = _condition,
                value = _value,
                parameter = _parameter
            };

            var fsm = _block.behaviourData.layers[0].stateMachine;
            if (_block.animationList != null)
            {
                var animList = _block.animationList;
                fsm.states = null;
                _block.behaviourData.parameters = null;

                for (var i = 0; i < animList.Count; i++)
                {
                    //Add a state named clip.name, whose position is (250,100,0)
                    var state = fsm.AddState(animList[i].name, new Vector3(250, 100*i, 0));

                    //Add new parameters to list
                    _block.behaviourData.AddParameter("On" + animList[i].name, AnimatorControllerParameterType.Trigger);

                    //The animation of this state is clip
                    state.motion = animList[i];

                    //Add transition status
                    var fromAnyState = fsm.AddAnyStateTransition(state);
                    // fromAnyState.AddCondition(AnimatorConditionMode.Equals, 0, "On" + animList[i].name);
                    if(_parameter == "On" + animList[i].name)
                    {
                        fromAnyState.AddCondition(AnimatorConditionMode.Equals, 0, "On" + animList[i].name);

                        _block.behaviourData.AddParameter(_target + " " +_field, AnimatorControllerParameterType.Int);
                        fromAnyState.AddCondition(_condition, _value, _target + _field);
                    }
                    else
                    {
                        fromAnyState.AddCondition(AnimatorConditionMode.Equals, 0, "On" + animList[i].name);
                    }
                    //Set this state to the default state
                    if (animList[i].name == "Idle")
                    {
                        fsm.defaultState = state;
                    }
                }
            }

            var data = _block.behaviourTable;

            return decision;
        }
    }
}