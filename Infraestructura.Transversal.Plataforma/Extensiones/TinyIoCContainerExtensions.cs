using System;
using System.Collections.Generic;
using System.Linq;
using Infraestructura.Transversal.Plataforma.TinyIoC;

namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class TinyIoCContainerExtensions
    {
        public static object Resolve(this TinyIoCContainer container, string fullNameType)
        {
            object objResolved = null;
            var field = container.GetType().GetField("_RegisteredTypes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var registeredTypes = field.GetValue(container);
                var keysField = registeredTypes.GetType().GetProperty("Keys");
                if (keysField != null)
                {
                    var Keys = (IEnumerable<TinyIoCContainer.TypeRegistration>)keysField.GetValue(registeredTypes);
                    var keysProperty = Keys.GetType().GetProperty("Keys");

                    var Typeobj = Keys.Where(k => k.Type.FullName == fullNameType).FirstOrDefault();
                    objResolved = container.Resolve(Typeobj.Type);
                }
            }

            return objResolved;
        }
        public static IResult ResolveInterface<IResult>(this TinyIoCContainer container)
        {
            IResult objResolved = default(IResult);
            var field = container.GetType().GetField("_RegisteredTypes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var registeredTypes = field.GetValue(container);
                var keysField = registeredTypes.GetType().GetProperty("Keys");
                if (keysField != null)
                {
                    var Keys = (IEnumerable<TinyIoCContainer.TypeRegistration>)keysField.GetValue(registeredTypes);
                    var keysProperty = Keys.GetType().GetProperty("Keys");


                    var ItypeSearch = typeof(IResult);
                    var Typeobj = Keys.Where(k => k.Type.GetInterfaces().Contains(ItypeSearch)).FirstOrDefault();

                    objResolved = Typeobj != null ? (IResult)container.Resolve(Typeobj.Type) : default(IResult);
                }
            }
            return objResolved;
        }
        public static bool TryResolveInterface<IResult>(this TinyIoCContainer container, out IResult result)
        {
            result = container.ResolveInterface<IResult>();
            return result != null;
        }
        public static ITResult Inject<ITResult>(this TinyIoCContainer container, ref ITResult instance)

        {
            if (instance != null && instance is ITResult) return instance;
            ITResult result;
            if (container.CanResolve(typeof(ITResult)))
            {
                result = (ITResult)container.Resolve(typeof(ITResult));
            }
            else
            {
                container.TryResolveInterface<ITResult>(out result);
            }

            if (result != null)
            {
                instance = (ITResult)result;
            }

            return instance;
        }
    }
}

