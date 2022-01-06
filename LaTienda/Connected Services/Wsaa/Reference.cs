﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wsaa
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginFault", Namespace="https://wsaahomo.afip.gov.ar/ws/services/LoginCms")]
    public partial class LoginFault : object
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://wsaahomo.afip.gov.ar/ws/services/LoginCms", ConfigurationName="Wsaa.LoginCMS")]
    public interface LoginCMS
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(Wsaa.LoginFault), Action="", Name="fault")]
        System.Threading.Tasks.Task<Wsaa.loginCmsResponse> loginCmsAsync(Wsaa.loginCmsRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="loginCms", WrapperNamespace="http://wsaa.view.sua.dvadac.desein.afip.gov", IsWrapped=true)]
    public partial class loginCmsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://wsaa.view.sua.dvadac.desein.afip.gov", Order=0)]
        public string in0;
        
        public loginCmsRequest()
        {
        }
        
        public loginCmsRequest(string in0)
        {
            this.in0 = in0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="loginCmsResponse", WrapperNamespace="http://wsaa.view.sua.dvadac.desein.afip.gov", IsWrapped=true)]
    public partial class loginCmsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://wsaa.view.sua.dvadac.desein.afip.gov", Order=0)]
        public string loginCmsReturn;
        
        public loginCmsResponse()
        {
        }
        
        public loginCmsResponse(string loginCmsReturn)
        {
            this.loginCmsReturn = loginCmsReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface LoginCMSChannel : Wsaa.LoginCMS, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class LoginCMSClient : System.ServiceModel.ClientBase<Wsaa.LoginCMS>, Wsaa.LoginCMS
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public LoginCMSClient() : 
                base(LoginCMSClient.GetDefaultBinding(), LoginCMSClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.LoginCms.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LoginCMSClient(EndpointConfiguration endpointConfiguration) : 
                base(LoginCMSClient.GetBindingForEndpoint(endpointConfiguration), LoginCMSClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LoginCMSClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(LoginCMSClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LoginCMSClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(LoginCMSClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LoginCMSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Wsaa.loginCmsResponse> Wsaa.LoginCMS.loginCmsAsync(Wsaa.loginCmsRequest request)
        {
            return base.Channel.loginCmsAsync(request);
        }
        
        public System.Threading.Tasks.Task<Wsaa.loginCmsResponse> loginCmsAsync(string in0)
        {
            Wsaa.loginCmsRequest inValue = new Wsaa.loginCmsRequest();
            inValue.in0 = in0;
            return ((Wsaa.LoginCMS)(this)).loginCmsAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.LoginCms))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.LoginCms))
            {
                return new System.ServiceModel.EndpointAddress("https://wsaahomo.afip.gov.ar/ws/services/LoginCms");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return LoginCMSClient.GetBindingForEndpoint(EndpointConfiguration.LoginCms);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return LoginCMSClient.GetEndpointAddress(EndpointConfiguration.LoginCms);
        }
        
        public enum EndpointConfiguration
        {
            
            LoginCms,
        }
    }
}
