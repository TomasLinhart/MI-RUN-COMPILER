using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappy.Typing
{
    public class TypeSignature
    {
        public string Name { get; private set; }
        public List<PropertySignature> PropertySignatures { get; private set; }
        public List<MethodSignature> MethodSignatures { get; private set; }

        public TypeSignature(string name)
        {
            Name = name;
            PropertySignatures = new List<PropertySignature>();
            MethodSignatures = new List<MethodSignature>();
        }

        public void AddProperty(string name, string type)
        {
            PropertySignatures.Add(new PropertySignature(name, type));
        }

        public void AddMethod(string name, string type, List<string> args)
        {
            MethodSignatures.Add(new MethodSignature(name, type, args));
        }

        public bool HasProperty(string name, string type)
        {
            return PropertySignatures.Any(propertySignature => propertySignature.Name == name && propertySignature.Type == type);
        }

        public bool HasMethod(string name, string type, List<string> args)
        {
            foreach (var methodSignature in MethodSignatures)
            {
                if (methodSignature.Name == name && methodSignature.Type == type)
                {
                    if (methodSignature.Arguments.Count != args.Count)
                    {
                        return false;
                    }

                    return !args.Where((t, i) => methodSignature.Arguments[i] != t).Any();
                }
            }

            return false;
        }
    }
}
