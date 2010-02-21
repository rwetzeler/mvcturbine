namespace MvcTurbine.ComponentModel {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.ComponentModel.Composition.Primitives;
	using System.Linq;
	
	public class ServiceLocatorExportProvider : ExportProvider {
    
		private IServiceLocator serviceLocator;
        private IDictionary<string, Type> contractMapping;
    
	    public ServiceLocatorExportProvider(IServiceLocator serviceLocator) {
	        this.contractMapping = new Dictionary<string, Type>();
	        this.serviceLocator = serviceLocator;
	    }
 
	    //The IoC container needs to call this in order to tell MEF how to call it back.
	    public void RegisterType(Type type)
	    {
	       contractMapping.Add(AttributedModelServices.GetContractName(type), type);
	    }
 
	    //Returns a lazy reference to a service coming out of the IoC
	    protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
	    {
	        if (definition.ContractName != null)
	        {
	            Type contractType;

	            if(contractMapping.TryGetValue(definition.ContractName, out contractType))
	            {
	                if (definition.Cardinality == ImportCardinality.ExactlyOne || definition.Cardinality == ImportCardinality.ExactlyOne)
	                {
	                    var export = new Export(definition.ContractName, () => serviceLocator.Resolve<object>(contractType));
	                    return new List<Export> { export };
	                }    
	            }
	        }
			
	        return Enumerable.Empty<Export>();
	    }
	}
}
