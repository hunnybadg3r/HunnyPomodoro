# Create Session

## Create Session Request

```js
POST /users/{userId}/session
```

```json
{
  "startDateTime": "2024-01-01T00:00:00"
}
```

## Create Session Response

```js
201 Created
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "userId": "00000000-0000-0000-0000-000000000000",
  "startDateTime": "2020-01-01T00:00:00",
  "endDateTime": "",
  "sessionStatus": "InProgress",
}
```
# Update Session

## Update Session Request

```js
POST /users/{userId}/session/update
```

```json
  "sessionId": "00000000-0000-0000-0000-000000000000",
  "endDateTime": "2020-01-01T00:25:00",
  "sessionStatus": "Done",
```

## Update Session Response

```js
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "userId": "00000000-0000-0000-0000-000000000000",
  "startDateTime": "2020-01-01T00:00:00",
  "endDateTime": "2020-01-01T00:25:00",
  "sessionStatus": "Done",
}
```

# List Session

## List Session Request

```js
GET /users/{userId}/sessions
```

## List Session Response

```js
200 OK
```

```json
[
    {
    "id": "7f90480f-f1fb-40f2-aa6d-bd3a3f293201",
    "userId": "29957128-c7be-4071-aa64-1ad53f068e25",
    "startTime": "2020-01-01T00:00:00",
    "endTime": "0001-01-01T00:00:00",
    "status": "InProgress"
  },
  {
    "id": "3f579a8c-febb-4b7d-ba08-d53b53264853",
    "userId": "29957128-c7be-4071-aa64-1ad53f068e25",
    "startTime": "2020-01-01T00:00:00",
    "endTime": "2024-03-13T02:00:00",
    "status": "Done"
  }
]