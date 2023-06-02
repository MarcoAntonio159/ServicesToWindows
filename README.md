# ServicesToWindows
## Requerimiento del Servicio:
1. Desarrollar un servicio de Windows en .Net Framework
2. El objetivo del servicio es que cada 10 min consulte la siguiente URL: https://jsonplaceholder.typicode.com/todos/{ID} en donde el ID es un atributo dinámico.
3. Al momento de consulta, debe insertar en una tabla (SQL-> usar lo estándar de conexión con la cadena de conexión): los atributos disponibles de esa URL, incluyendo el ID. Si no existe ningún registro en esa tabla, consultara el ID 1, si ya existe registros, consultara el N actual + 1.
4. Por cada consulta que haga debe guardar también en un log de seguimiento toda la información.
5. El proceso de consulta solo debe durar hasta el ID 30, esto que quiere decir, que una vez llegado al ID 30, ya no debería consultar la información ni a la URL simplemente debería pasar de largo.
Recordar que la idea central del ejercicio es ver sus modularizaciones, código y buenas prácticas, el objetivo podrá ser el mismo pero la lógica y la habilidad para cumplirlo se verá dentro del código.
Así mismo, me deben pasar el código y el instalador como ejecutable.
Los parámetros de conexión a SQL deben estar en el app config, y en base a esto cuando lo instale, debo ser capaz de abrir mi archivo config xml y cambiar la cadena para que pueda funcionar en mi base de datos.
## Solucion propuesta:
1. Se creo el proyecto con Windows Services (.NET)
2. Contiene un archivo [App.config](First-activity-wara/App.config), donde puedes modificar el intervalo de tiempo que quieres que consulte a la API. Tambien puedes configurar al conexion a la base de datos.
3. Contiene un archivo [service1.cs](First-activity-wara/Service1.cs), donde se encuentra la logica de lo solicitado.
4. Contiene un archivo [MyServiceInstaller.cs](First-activity-wara/MyServiceInstaller.cs), donde se encuentra el codigo para crear el instalador, y el nombre del servicio.
5. Contine un archivo [Query-DB-COnsultingAPI.sql](Sentencia-SQL/Query-DB-ConsultingApi.sql), donde se encuentra la query para crear nuestra base de datos y su tabla correspondiente.
## Como ejecutar:
1. Abre CMD como administrador.
2. Dirigete a la carpeta [First-activity-wara/bin/Debug](First-activity-wara/bin/Debug).
3. En el CMD ejecuta el siguiente comando: <b>installutil First-activity-wara.exe</b> esto lo que realizara es instalar el servicio en tu equipo.
4. En la misma ventana de CMD, ahora ejecuta el comando <b>NET START MyServiceWara</b> esto ejecutara el servicio en tu equipo.<br>
Ten en cuenta que si editar el nombre del servicio en el archivo [MyServiceInstaller.cs](First-activity-wara/MyServiceInstaller.cs), tambien tienes que ejecutar el comando NET START con el nombre de tu servicio al cual cambiaste.
5. Ahora en tu Base de datos ya se estan guardando los registros de acuerdo al tiempo que configuraste en el archivo [App.config](First-activity-wara/App.config).

## Importante
* Si deseas parar el servicio, ejecuta el siguiente comando <b>sc stop MyServiceWara</b>
* Si deseas desinstalar el servicio, ejecuta el siguiente comando <b>sc delete MyServiceWara</b>