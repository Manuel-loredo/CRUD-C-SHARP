using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD
{
   public class CapaNegocios
    {
        private CapaDatos capaDatos;

        public CapaNegocios()
        {
            capaDatos = new CapaDatos();
        }
        public Contacto GuardarContacto(Contacto contacto)
        {

                    if(contacto.Id == 0){
                       capaDatos.InsertarContacto(contacto);
                   } else {
                       capaDatos.ActualizarContacto(contacto);
               }

            return contacto;

        }

        public List<Contacto> GetContactos( string TextoBuscar = null)
        {
          return capaDatos.GetContactos(TextoBuscar);
        }

        public void EliminarContacto(int id)
        {
            capaDatos.EliminarContacto(id);
        }
    }
}
