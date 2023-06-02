# ServicesToWindows
Requerimiento del Servicio:
1) Desarrollar un servicio de Windows en .Net Framework
2) El objetivo del servicio es que cada 10 min consulte la siguiente URL: https://jsonplaceholder.typicode.com/todos/{ID} en donde el ID es un atributo dinámico.
3) Al momento de consulta, debe insertar en una tabla (SQL-> usar lo estándar de conexión con la cadena de conexión): los atributos disponibles de esa URL, incluyendo el ID. Si no existe ningún registro en esa tabla, consultara el ID 1, si ya existe registros, consultara el N actual + 1.
4) Por cada consulta que haga debe guardar también en un log de seguimiento toda la información.
5) El proceso de consulta solo debe durar hasta el ID 30, esto que quiere decir, que una vez llegado al ID 30, ya no debería consultar la información ni a la URL simplemente debería pasar de largo.
Recordar que la idea central del ejercicio es ver sus modularizaciones, código y buenas prácticas, el objetivo podrá ser el mismo pero la lógica y la habilidad para cumplirlo se verá dentro del código.
Así mismo, me deben pasar el código y el instalador como ejecutable.
Los parámetros de conexión a SQL deben estar en el app config, y en base a esto cuando lo instale, debo ser capaz de abrir mi archivo config xml y cambiar la cadena para que pueda funcionar en mi base de datos.
