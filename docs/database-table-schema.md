# Database Table Schema

This is the starting database table schema.

https://github.com/bakineggs/recurring_events_for

## Content

1. [Users](#users)
1. [Events](#events)
1. [Event_Recurrences](#event_recurrences)
1. [Event_Cancelations](#event_cancelations)
1. [Event_Completions](#event_completions)
1. [Event_Notes](#event_notes)



## Users

| Field      | Type         | Null | Key | Default           | Extra |
|------------|--------------|------|-----|-------------------|-------|
| id         | char(36)     | NO   | PRI | None              |       |
| email      | varchar(100) | NO   | UNI | None              |       |
| password   | varchar(250) | NO   |     | None              |       |
| created_on | timestamp    | NO   |     | CURRENT_TIMESTAMP |       |

[:point_up: Back to top](#content)

## Events

| Field              | Type                                             | Null | Key | Default           | Extra |
|--------------------|--------------------------------------------------|------|-----|-------------------|-------|
| id                 | char(36)                                         | NO   | PRI | None              |       |
| user_id            | char(36)                                         | NO   | MUL | None              |       |
| name               | varchar(100)                                     | NO   |     | None              |       |
| description        | text                                             | YES  |     | None              |       |
| phone_number       | char(10)                                         | YES  |     | None              |       |
| location_address_1 | varchar(70)                                      | YES  |     | None              |       |
| location_address_2 | varchar(70)                                      | YES  |     | None              |       |
| location_city      | varchar(40)                                      | YES  |     | None              |       |
| location_state     | char(2)                                          | YES  |     | None              |       |
| location_zip       | varchar(5)                                       | YES  |     | None              |       |
| starts_on          | date                                             | NO   |     | None              |       |
| ends_on            | date                                             | NO   |     | None              |       |
| starts_at          | time                                             | YES  |     | None              |       |
| ends_at            | time                                             | YES  |     | None              |       |
| frequency          | enum('ONCE','DAILY','WEEKLY','MONTHLY','YEARLY') | YES  |     | ONCE              |       |
| separation         | int(10) unsigned                                 | YES  |     | 1                 |       |
| created_on         | timestamp                                        | NO   |     | CURRENT_TIMESTAMP |       |
| recurrence_day     | int(11)                                          | YES  |     | None              |       |
| recurrence_week    | int(11)                                          | YES  |     | None              |       |
| recurrence_month   | int(11)                                          | YES  |     | None              |       |

[:point_up: Back to top](#content)

## Event_Recurrences

* id 
* event_id
* month
* day
* week

[:point_up: Back to top](#content)

## Event_Cancelations

| Field       | Type      | Null | Key | Default           | Extra |
+-------------|-----------|------|-----|-------------------|-------|
| event_id    | char(36)  | NO   | PRI | None              |       |
| date        | date      | NO   | PRI | None              |       |
| recorded_on | timestamp | NO   |     | CURRENT_TIMESTAMP |       |


[:point_up: Back to top](#content)

## Event_Completions

| Field            | Type      | Null | Key | Default           | Extra |
|------------------|-----------|------|-----|-------------------|-------|
| event_id         | char(36)  | NO   | PRI | None              |       |
| date             | date      | NO   | PRI | None              |       |
| marked_completed | timestamp | NO   |     | CURRENT_TIMESTAMP |       |

[:point_up: Back to top](#content)

## Event_Notes

| Field      | Type     | Null | Key | Default | Extra |
|------------|----------|------|-----|---------|-------|
| id         | char(36) | NO   | PRI | None    |       |
| event_id   | char(36) | NO   | MUL | None    |       |
| created_on | datetime | NO   |     | None    |       |
| content    | text     | YES  |     | None    |       |

[:point_up: Back to top](#content)
