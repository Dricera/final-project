- [x] Implement Ticket [Endpoints](#endpoints)
- [ ] Implement User Endpoints
- [ ] Standardize the [Ticket Skeleton Object](#ticket-skeleton-object)
- [ ] Database integration
- [ ] Import/Export portability
- [ ] ML.NET


## Endpoints

| Request | Endpoint                     | Description                 |
| ------- | ---------------------------- | --------------------------- |
| GET     | tickets                      | list all                    |
| GET     | tickets/show?ids={ids}       | list specific multiple      |
| POST    | tickets                      | create                      |
| POST    | tickets/bulkcreate           | create many                 |
| GET     | tickets/{id}                 | fetch {id}                  |
| PUT     | tickets/{id}                 | update {id}                 |
| PUT     | tickets/bulkupdate           | update ids listed in object |
| DELETE  | tickets/{id}                 | delete {id}                 |
| DELETE  | tickets/bulkdelete?ids={ids} | delete {id1,id2...}         |


## Ticket Skeleton Object

```json
{
  "assignee_id": "assigneee_user_id",
  "collaborator_ids": "collaborator_ids",
  "id": "ticket_id",
  "created_at": "2022-12-31T23:59:59Z",
  "description": "description",
  "priority": "high/medium/low",
  "subject": "ticket title",
  "status": "open/closed/pending...",
  "tags": ["feature", "incident", "internal", "external", "other_tags"],
  "type": "request/complaint",
  "updated_at": "2023-01-01T00:00:01Z",
  "url": "http://{domain}/api/tickets/{id}.json",
  "via": {
    "channel": "web"
  }
}
**
```






