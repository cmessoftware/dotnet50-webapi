using System.Collections;

namespace cmes_webapi.Common.Configuration
{
    /// <summary> 
    /// Clase que aloja la informacion de configuracion. 
    /// Es un Singleton. Implementa tambien Template Method
    /// </summary> 
    public class Authentication
    {
        protected static Authentication instance = null;
        protected static readonly object padlock = new object();
        private Hashtable data = null;


        /// <summary> 
        /// Constructor. Inicializa la coleccion 
        /// interna de datos 
        /// </summary> 
        protected Authentication()
        {
            data = new Hashtable();
        }

        /// <summary>
        /// Guarda los datos en el cache interno
        /// </summary>
        /// <param name="key">Clave de valor a guardar</param>
        /// <param name="val">Valor a guardar</param>
        private void StoreDataInCache(string key, Credential val)
        {
            lock (instance.data.SyncRoot)
            {
                // si el elemento ya esta en la lista de datos... 
                if (instance.data.ContainsKey(key))
                {
                    // lo quito 
                    instance.data.Remove(key);
                }

                // y lo vuelvo a añadir 
                instance.data.Add(key, val);
            }
        }


        /// <summary> 
        /// Obtener la instancia unica (Singleton) 
        /// </summary> 
        /// <returns>Retorna la instancia</returns> 
        public static Authentication Instance
        {
            get
            {
                // implementacion de singleton thread-safe usando double-check locking 
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Authentication();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary> 
        /// Retorna true si el repositorio de 
        /// parametros contiene la clave especificada 
        /// </summary> 
        /// <param name="key">Clave a buscar</param> 
        /// <returns>True si existe la clave</returns> 
        public bool Contains(string key)
        {
            return instance.data.ContainsKey(key);
        }

        /// <summary> 
        /// Limpia los datos de configuracion 
        /// </summary> 
        public void Clear()
        {
            lock (instance.data.SyncRoot)
            {
                instance.data.Clear();
            }
        }

        public class Credential
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
