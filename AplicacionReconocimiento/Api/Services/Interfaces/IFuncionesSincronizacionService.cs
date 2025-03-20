using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IFuncionesSincronizacionService
    {
        /*Obtener todos los clientes
         1. Eliminar la tabla anterior
         2. Pegarle al Webservice de DeportNet para obtener todos los clientes
         2. Inserto la lista de clientes en la tabla de clientes
        
        Chatgpt2. Recorrer la lista de clientes y por cada cliente:
            2.1. Verificar si el cliente existe en la BD local
            2.2. Si el cliente no existe, dar de alta el cliente en la BD local
            2.3. Si el cliente existe, verificar si hay cambios en los datos del cliente
            2.4. Si hay cambios, actualizar los datos del cliente en la BD local
            2.5. Si no hay cambios, no hacer nada
         3.
         */

    }
}
