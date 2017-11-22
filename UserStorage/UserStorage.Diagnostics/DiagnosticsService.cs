using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using UserStorage.Diagnostics.Query;

namespace UserStorage.Diagnostics
{
    [ServiceBehavior(Name = "Diagnostics", InstanceContextMode = InstanceContextMode.Single, ConfigurationName = "DiagnosticsService")]
    public abstract class DiagnosticsService : IServiceMonitor
    {
        protected abstract ReadOnlyCollection<ServiceInfo> ServiceInfoCollection { get; }

        public int GetCount()
        {
            return ServiceInfoCollection.Count;
        }

        public ServiceInfo[] Query(ServiceQuery query)
        {
            if (query.GetType() == typeof(All))
            {
                return ServiceInfoCollection.ToArray();
            }

            if (query.GetType() == typeof(NameEquals))
            {
                var nameEquals = (NameEquals)query;
                return ServiceInfoCollection.Where(s => s.Name == nameEquals.Name).ToArray();
            }

            if (query.GetType() == typeof(NameContains))
            {
                var nameContains = (NameContains)query;
                return ServiceInfoCollection.Where(s => s.Name.Contains(nameContains.Text)).ToArray();
            }

            if (query.GetType() == typeof(TypeEquals))
            {
                var typeEquals = (TypeEquals)query;
                return ServiceInfoCollection.Where(s => s.Type == typeEquals.Type).ToArray();
            }

            if (query.GetType() == typeof(ServiceQuery))
            {
                return new ServiceInfo[] { };
            }

            throw new InvalidOperationException(query.GetType().Name);
        }
    }
}
