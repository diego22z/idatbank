GET(se puede probar en postman- para ver la base de datos)

http://localhost:xxxx/api/clientes/Listar (lo veremos al abrir el visual studio en su maquiuna)
http://localhost:xxxx/api/clientes/Buscar/1



POST:http://localhost:xxx/api/clientes/Crear

- EN header ponemos tipo content-type , value= application/json
-body y de ahi a raw JSON, y implementamos a un nuevo cliente, para poder hacer pruebas


(por ejemplo podemos poner este dato de prueba de un nuevo vliente )
{"nombres": "Jeff",
"apellidos": "Patricio",
"dni":"29423819",
"email":"jeff123@gmail.com"
}


PUT: http://localhost:xxx/api/clientes/Editar/4

(por ejemplo podemos poner este dato de prueba)
{"clienteId":"4",
"nombres": "Jeff",
"apellidos": "Patricio",
"dni":"29423819",
"email":"jeff123@gmail.com"
}






DELETE:http://localhost:xxx/api/clientes/Eliminar/4
