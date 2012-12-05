using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrappy.Typing;

namespace Scrappy.Parser
{
    /// <summary>
    /// Provides really simple type system to verify basic type errors
    /// </summary>
    public class TypeSystem
    {
        private List<TypeSignature> types;

        public TypeSystem()
        {
            types = new List<TypeSignature>();
        }


        public void RegisterType(string type)
        {
            if (types.Any(typeSignature => typeSignature.Name == type))
            {
                return;
            }

            types.Add(new TypeSignature(type));
        }

        public bool HasProperty(string type, string name, string propertyType)
        {
            foreach (var typeSignature in types)
            {
                if (typeSignature.Name == type)
                {
                    return typeSignature.HasProperty(name, propertyType);
                }
            }

            return false;
        }

        public bool HasMethod(string type, string name, List<string> args, string returnType)
        {
            foreach (var typeSignature in types)
            {
                if (typeSignature.Name == type)
                {
                    return typeSignature.HasMethod(name, returnType, args);
                }
            }

            return false;
        }

        public void RegisterProperty(string type, string name, string propertyType)
        {
            foreach (var typeSignature in types)
            {
                if (typeSignature.Name == type)
                {
                    if (!typeSignature.HasProperty(name, propertyType))
                    {
                        typeSignature.AddProperty(name, propertyType);
                    }
                }
            }
        }

        public void RegisterMethod(string type, string name, List<string> args, string returnType)
        {
            foreach (var typeSignature in types)
            {
                if (typeSignature.Name == type)
                {
                    if (!typeSignature.HasMethod(name, returnType, args))
                    {
                        typeSignature.AddMethod(name, returnType, args);
                    }
                }
            }
        }
    }
}
