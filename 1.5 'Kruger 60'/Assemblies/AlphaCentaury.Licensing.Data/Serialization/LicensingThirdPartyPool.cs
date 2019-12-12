using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing.ThirdParty.Pool", Namespace = LicensingData.Namespace)]

    public class LicensingThirdPartyPool
    {
        private IReadOnlyDictionary<string, ThirdPartyDependency> _components;

        [XmlElement("Component")]
        public List<ThirdPartyDependency> Components { get; set; }

        public bool ComponentsSpecified => (Components != null) && (Components.Count > 0);

        public ThirdPartyDependency this[[CanBeNull] ThirdPartyDependency component] => ComponentsDictionary.TryGetValue(GetComponentKey(component), out var value) ? value : null;

        private IReadOnlyDictionary<string, ThirdPartyDependency> ComponentsDictionary
        {
            get
            {
                if (_components != null) return _components;

                _components = ComponentsSpecified switch
                {
                    true => Components.ToDictionary(GetComponentKey),
                    false => new Dictionary<string, ThirdPartyDependency>()
                };

                return _components;
            } // get
        } // ComponentsDictionary

        private string GetComponentKey([CanBeNull] ThirdPartyDependency component) => (component == null)? "" : component.GetKey();
    } // class LicensingThirdPartyPool
} // namespace