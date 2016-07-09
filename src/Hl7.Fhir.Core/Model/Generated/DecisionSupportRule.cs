﻿using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

/*
  Copyright (c) 2011+, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

//
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A decision support rule
    /// </summary>
    [FhirType("DecisionSupportRule", IsResource=true)]
    [DataContract]
    public partial class DecisionSupportRule : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DecisionSupportRule; } }
        [NotMapped]
        public override string TypeName { get { return "DecisionSupportRule"; } }
        
        /// <summary>
        /// Module information for the rule
        /// </summary>
        [FhirElement("moduleMetadata", InSummary=true, Order=90)]
        [DataMember]
        public ModuleMetadata ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private ModuleMetadata _ModuleMetadata;
        
        /// <summary>
        /// Logic used within the rule
        /// </summary>
        [FhirElement("library", InSummary=true, Order=100)]
        [References("Library")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ResourceReference>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Library;
        
        /// <summary>
        /// "when" the rule should be invoked
        /// </summary>
        [FhirElement("trigger", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TriggerDefinition> Trigger
        {
            get { if(_Trigger==null) _Trigger = new List<TriggerDefinition>(); return _Trigger; }
            set { _Trigger = value; OnPropertyChanged("Trigger"); }
        }
        
        private List<TriggerDefinition> _Trigger;
        
        /// <summary>
        /// "if" some condition is true
        /// </summary>
        [FhirElement("condition", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ConditionElement
        {
            get { return _ConditionElement; }
            set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ConditionElement;
        
        /// <summary>
        /// "if" some condition is true
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Condition
        {
            get { return ConditionElement != null ? ConditionElement.Value : null; }
            set
            {
                if(value == null)
                  ConditionElement = null; 
                else
                  ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Condition");
            }
        }
        
        /// <summary>
        /// "then" perform these actions
        /// </summary>
        [FhirElement("action", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActionDefinition> Action
        {
            get { if(_Action==null) _Action = new List<ActionDefinition>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<ActionDefinition> _Action;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DecisionSupportRule;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ModuleMetadata != null) dest.ModuleMetadata = (ModuleMetadata)ModuleMetadata.DeepCopy();
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(Trigger != null) dest.Trigger = new List<TriggerDefinition>(Trigger.DeepCopy());
                if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                if(Action != null) dest.Action = new List<ActionDefinition>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DecisionSupportRule());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DecisionSupportRule;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DecisionSupportRule;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }
        
    }
    
}