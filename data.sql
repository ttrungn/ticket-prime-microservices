-- Insert Categories
INSERT INTO Categories (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('e94d4f14-f7d8-4ee0-af0e-9c023a729a06', 'Music', 'MUSIC', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO Categories (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('357c9afe-5b57-446a-8884-b50e1568028c', 'Sports', 'SPORTS', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO Categories (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('83907cd7-6a58-4d06-a28b-dc1b886773bb', 'Education', 'EDUCATION', SYSDATETIMEOFFSET(), NULL, NULL, 0);

-- Insert SubCategories
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('c19bc5b4-aea3-498c-9c1f-704b03bb14af', 'Rock', 'ROCK', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('2028ad6b-d3bc-4294-a53d-15242b7e606c', 'Pop', 'POP', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('07887d5e-d0cc-4bf4-971a-7745808fdaa5', 'Football', 'FOOTBALL', '357c9afe-5b57-446a-8884-b50e1568028c', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('d9180fff-5932-489f-907d-79ee4f5f93d7', 'Math', 'MATH', '83907cd7-6a58-4d06-a28b-dc1b886773bb', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('a7d76e52-4eb8-4d86-81d9-d63192f74df8', 'Jazz', 'JAZZ', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('4e9145ea-f757-48b8-a504-4887e8c652bf', 'Hip Hop', 'HIPHOP', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('185db075-cb01-456a-b46a-70ca45b97fb8', 'Classical', 'CLASSICAL', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('e7cbf80a-a566-4f60-8af0-3ffe0f53612a', 'Basketball', 'BASKETBALL', '357c9afe-5b57-446a-8884-b50e1568028c', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('3d52c49c-ecaa-4899-85e2-027ee6d45707', 'Tennis', 'TENNIS', '357c9afe-5b57-446a-8884-b50e1568028c', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('fb1e76b5-cbe0-43d9-af13-bbf283515777', 'Cricket', 'CRICKET', '357c9afe-5b57-446a-8884-b50e1568028c', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('4638ead7-4964-4b55-b44a-5760041df47d', 'Physics', 'PHYSICS', '83907cd7-6a58-4d06-a28b-dc1b886773bb', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('5f0e2399-31f1-44ef-a793-7d258799d714', 'Chemistry', 'CHEMISTRY', '83907cd7-6a58-4d06-a28b-dc1b886773bb', SYSDATETIMEOFFSET(), NULL, NULL, 0);
INSERT INTO SubCategories (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('e245ecdd-28c5-4ed3-8d9c-b6b244ed41ac', 'History', 'HISTORY', '83907cd7-6a58-4d06-a28b-dc1b886773bb', SYSDATETIMEOFFSET(), NULL, NULL, 0);