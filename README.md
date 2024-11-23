# Monster Hunter World API ⭐

![image](https://github.com/user-attachments/assets/c59b7501-c098-4e7e-947d-ffe6d6a93e13)

Para poner en funcionamiento tendras que cambiar los paramentros de la base de datos 

y ejecutar el archivo backup de la base de datos en el archivo sql

## Integrantes

Ruben Feng

Gael Pereira

CesarAburto


## EndPoints
## GET

https://localhost:7101/monstro

Retornara
```
[
  {
    "idMonstro": 0,
    "nombre": "string",
    "elementos" : {
    "elemento": "string
    }
    "imagen": {
      "id_imagen": 0,
      "imageUrl": "string",
      "iconUrl": "string"
    },
    "detalle": "string"
  }
]

```

https://localhost:7101/monstro/{id}

Tienes que ingresar el ID del monstro a buscar

Retornara:
```
{
    "idMonstro": 0,
    "nombre": "string",
    "vida": 0,
    "tipo": {
      "id_categoria": 0,
      "categoria": "string"
    },
    "imagen": {
      "id_imagen": 0,
      "imageUrl": "string",
      "iconUrl": "string"
    },
    "biomas": [
      {
        "id_bioma": 0,
        "bioma": "string"
      }
    ],
    "rangos": [
      {
        "id_rango": 0,
        "rango": "string"
      }
    ],
    "elementos": [
      {
        "id_elemento": 0,
        "elemento": "string"
      }
    ],
    "debilidad": [
      {
        "id_elemento": 0,
        "elemento": "string",
        "eficacia": 0
      }
    ],
    "items": [
      {
        "id": 0,
        "name": "string",
        "description": "string"
      }
    ]
  }


```
Si se ejecuto correctamente 

Retornara

```
200
```
Si no se encotro el monstro

Retornara 

```
404, Monstro no se encotro
```


## POST

https://localhost:7101/monstro

Al hacer POST necesitaras introducir en el siguiente formato del BODY: 
```
{
  "nombre": "string",
  "vida": 0,
  "tipo": {
    "id_categoria": 0
  },
  "imagen": {
    "imageUrl": "string",
    "iconUrl": "string"
  },
  "biomas": [
    {
      "id_bioma": 0
    }
  ],
  "rangos": [
    {
      "id_rango": 0
    }
  ],
  "elementos": [
    {
      "id_elemento": 0
    }
  ],
  "debilidad": [
    {
      "id_elemento": 0,
      "eficacia": 0
    }
  ],
  "items": [
    {
      "name": "string",
      "description": "string"
    }
  ]
}
```
Al insertase correctamente

Retornara

```
201, se inserto correctamente
```
si el nombre del monstro existe 

retornara

```
409, El monstro ya existe id: {}
```

## PUT

https://localhost:7101/monstro/{id}

Al querer Actualizar un dato se necistara el ID y los datos a Actualizar
```
{
  "nombre": "string",
  "vida": 0,
  "tipo": {
    "id_categoria": 0
  },
  "imagen": {
    "imageUrl": "string",
    "iconUrl": "string"
  },
  "biomas": [
    {
      "id_bioma": 0
    }
  ],
  "rangos": [
    {
      "id_rango": 0
    }
  ],
  "elementos": [
    {
      "id_elemento": 0
    }
  ],
  "debilidad": [
    {
      "id_elemento": 0
      "eficacia": 0
    }
  ],
  "items": [
    {
      "name": "string",
      "description": "string"
    }
  ]
}
```

## DELETE

https://localhost:7101/monstro/{id}

AL querer Eliminar un registro se necesitara del ID

retornara
```
Monstro eliminado id: 1
```
# Endpoints para los usuario 
## POST
https://localhost:7101/usuario/nuevo

Datos necesarios
```
{
  "nombreusuario": "string",
  "password": "string"
}
```

si se inserto correctamente

Retornara
```
201, "Se Registro nuevo usuario
```

si el usuario ya existe 

Retornara 
```
409, "Existe usuaario con ese nombre
```

https://localhost:7101/usuario

EndPoint para validar el usuario 

tendras que proporcionar los datos de inicio
```
{
  "nombreusuario": "string",
  "password": "string"
}
```
si el usuario a ingresar no existe 

Retornara
```
401, Usuario no registrado
```

si existe

Retornara
```
Ok, 200
```

