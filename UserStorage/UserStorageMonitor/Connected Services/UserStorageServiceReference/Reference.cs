﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace UserStorageMonitor.UserStorageServiceReference {
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="EmptyQuery", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics.Query")]
    [Serializable()]
    [KnownType(typeof(SelectAllServicesQuery))]
    [KnownType(typeof(SelectServicesWithSpecifiedNameQuery))]
    [KnownType(typeof(SelectServicesWithNameContainsQuery))]
    [KnownType(typeof(SelectServicesWithSpecifiedTypeQuery))]
    public partial class EmptyQuery : object, IExtensibleDataObject, INotifyPropertyChanged {
        
        [NonSerialized()]
        private ExtensionDataObject extensionDataField;
        
        [Browsable(false)]
        public ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="SelectAllServicesQuery", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics.Query")]
    [Serializable()]
    public partial class SelectAllServicesQuery : EmptyQuery {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="SelectServicesWithSpecifiedNameQuery", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics.Query")]
    [Serializable()]
    public partial class SelectServicesWithSpecifiedNameQuery : EmptyQuery {
        
        [OptionalField()]
        private string NameField;
        
        [DataMember()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="SelectServicesWithNameContainsQuery", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics.Query")]
    [Serializable()]
    public partial class SelectServicesWithNameContainsQuery : EmptyQuery {
        
        [OptionalField()]
        private string NameTextField;
        
        [DataMember()]
        public string NameText {
            get {
                return this.NameTextField;
            }
            set {
                if ((ReferenceEquals(this.NameTextField, value) != true)) {
                    this.NameTextField = value;
                    this.RaisePropertyChanged("NameText");
                }
            }
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="SelectServicesWithSpecifiedTypeQuery", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics.Query")]
    [Serializable()]
    public partial class SelectServicesWithSpecifiedTypeQuery : EmptyQuery {
        
        [OptionalField()]
        private string TypeField;
        
        [DataMember()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="ServiceInfo", Namespace="http://schemas.datacontract.org/2004/07/UserStorage.Diagnostics")]
    [Serializable()]
    public partial class ServiceInfo : object, IExtensibleDataObject, INotifyPropertyChanged {
        
        [NonSerialized()]
        private ExtensionDataObject extensionDataField;
        
        [OptionalField()]
        private string ServiceDebugInfoField;
        
        [OptionalField()]
        private string ServiceNameField;
        
        [OptionalField()]
        private string ServiceTypeField;
        
        [OptionalField()]
        private string ServiceUrlField;
        
        [Browsable(false)]
        public ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [DataMember()]
        public string ServiceDebugInfo {
            get {
                return this.ServiceDebugInfoField;
            }
            set {
                if ((ReferenceEquals(this.ServiceDebugInfoField, value) != true)) {
                    this.ServiceDebugInfoField = value;
                    this.RaisePropertyChanged("ServiceDebugInfo");
                }
            }
        }
        
        [DataMember()]
        public string ServiceName {
            get {
                return this.ServiceNameField;
            }
            set {
                if ((ReferenceEquals(this.ServiceNameField, value) != true)) {
                    this.ServiceNameField = value;
                    this.RaisePropertyChanged("ServiceName");
                }
            }
        }
        
        [DataMember()]
        public string ServiceType {
            get {
                return this.ServiceTypeField;
            }
            set {
                if ((ReferenceEquals(this.ServiceTypeField, value) != true)) {
                    this.ServiceTypeField = value;
                    this.RaisePropertyChanged("ServiceType");
                }
            }
        }
        
        [DataMember()]
        public string ServiceUrl {
            get {
                return this.ServiceUrlField;
            }
            set {
                if ((ReferenceEquals(this.ServiceUrlField, value) != true)) {
                    this.ServiceUrlField = value;
                    this.RaisePropertyChanged("ServiceUrl");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="UserStorageServiceReference.Monitor")]
    public interface Monitor {
        
        [OperationContract(Action="http://tempuri.org/Monitor/GetServicesCount", ReplyAction="http://tempuri.org/Monitor/GetServicesCountResponse")]
        int GetServicesCount();
        
        [OperationContract(Action="http://tempuri.org/Monitor/GetServicesCount", ReplyAction="http://tempuri.org/Monitor/GetServicesCountResponse")]
        Task<int> GetServicesCountAsync();
        
        [OperationContract(Action="http://tempuri.org/Monitor/QueryServices", ReplyAction="http://tempuri.org/Monitor/QueryServicesResponse")]
        ServiceInfo[] QueryServices(EmptyQuery queryType);
        
        [OperationContract(Action="http://tempuri.org/Monitor/QueryServices", ReplyAction="http://tempuri.org/Monitor/QueryServicesResponse")]
        Task<ServiceInfo[]> QueryServicesAsync(EmptyQuery queryType);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface MonitorChannel : Monitor, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class MonitorClient : ClientBase<Monitor>, Monitor {
        
        public MonitorClient() {
        }
        
        public MonitorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MonitorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitorClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetServicesCount() {
            return base.Channel.GetServicesCount();
        }
        
        public Task<int> GetServicesCountAsync() {
            return base.Channel.GetServicesCountAsync();
        }
        
        public ServiceInfo[] QueryServices(EmptyQuery queryType) {
            return base.Channel.QueryServices(queryType);
        }
        
        public Task<ServiceInfo[]> QueryServicesAsync(EmptyQuery queryType) {
            return base.Channel.QueryServicesAsync(queryType);
        }
    }
}
