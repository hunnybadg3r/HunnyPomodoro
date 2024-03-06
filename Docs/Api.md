# Hunny Pomodoro API

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
  "Name": "badger",
  "email": "hunnybadg3r@gmail.com",
  "password": "badger333"
}
```

#### Register Response

```js
200 OK
```

```json
{
  "id": "2e54106f-4be0-4b69-987a-8e37a8bcf10d",
  "name": "badger",
  "email": "hunnybadg3r@gmail.com",
  "token": "eyJhb..hbbQ"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
  "email": "sometest@mail.com",
  "password": "badger1232!"
}
```

#### Login Response

```js
200 OK
```

```json
{
  "id": "2e54106f-4be0-4b69-987a-8e37a8bcf10d",
  "name": "badger",
  "email": "sometest@mail.com",
  "token": "eyJhb..hbbQ"
}
```
