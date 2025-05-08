USE AuthServiceDb
INSERT INTO AuthServiceDb.dbo.AspNetUsers (Id, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'0de033a0-425d-414b-9309-48a02803e1c5', N'2025-05-08 02:01:29.6237560 +00:00', N'2025-05-08 02:01:29.6365510 +00:00', N'0001-01-01 00:00:00.0000000 +00:00', 0, N'organizer@mail.com', N'ORGANIZER@MAIL.COM', N'organizer@mail.com', N'ORGANIZER@MAIL.COM', 0, N'AQAAAAIAAYagAAAAEJ0m56dd0kbGoM07ujfWaR7L40VXxu4YbQf/Vw2HvhLzsvFuNXWQUv/E0IFxGQoYSQ==', N'KQS3BQBPYCAQQFNXOXG3QOSZXOFPBTJY', N'dc0e5394-de44-42a6-b06d-218bcc4b2add', null, 0, 0, null, 1, 0);
INSERT INTO AuthServiceDb.dbo.AspNetUsers (Id, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'fa72fce6-e9e4-4dbf-a8fe-3bd972a2a1f7', N'2025-05-08 02:01:22.0487840 +00:00', N'2025-05-08 02:01:22.1811140 +00:00', N'0001-01-01 00:00:00.0000000 +00:00', 0, N'customer@mail.com', N'CUSTOMER@MAIL.COM', N'customer@mail.com', N'CUSTOMER@MAIL.COM', 0, N'AQAAAAIAAYagAAAAELl+hIeLap34wdVh7gJaG13AscBxTefYqF4kDuSG9ocjtHczhsXWCKr0gFIdctnBTA==', N'S4UMF6BSQ2RUEFP7FLL43DMIBQAO4YS3', N'cb61d003-4209-46bd-9ba0-6383cfc47cf6', null, 0, 0, null, 1, 0);

USE CoreServiceDb
-- Insert Categories
INSERT INTO Category (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('e94d4f14-f7d8-4ee0-af0e-9c023a729a06', 'Music', 'MUSIC', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO Category (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('357c9afe-5b57-446a-8884-b50e1568028c', 'Sports', 'SPORTS', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO Category (Id, Name, NormalizedName, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('83907cd7-6a58-4d06-a28b-dc1b886773bb', 'Education', 'EDUCATION', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);

-- Insert SubCategory
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('c19bc5b4-aea3-498c-9c1f-704b03bb14af', 'Rock', 'ROCK', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('2028ad6b-d3bc-4294-a53d-15242b7e606c', 'Pop', 'POP', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('07887d5e-d0cc-4bf4-971a-7745808fdaa5', 'Football', 'FOOTBALL', '357c9afe-5b57-446a-8884-b50e1568028c', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('d9180fff-5932-489f-907d-79ee4f5f93d7', 'Math', 'MATH', '83907cd7-6a58-4d06-a28b-dc1b886773bb', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('a7d76e52-4eb8-4d86-81d9-d63192f74df8', 'Jazz', 'JAZZ', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('4e9145ea-f757-48b8-a504-4887e8c652bf', 'Hip Hop', 'HIPHOP', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO SubCategory (Id, Name, NormalizedName, CategoryId, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('185db075-cb01-456a-b46a-70ca45b97fb8', 'Classical', 'CLASSICAL', 'e94d4f14-f7d8-4ee0-af0e-9c023a729a06', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);

-- Insert Organizer
INSERT INTO Organizer (Id, UserId, OrganizerCode, Name, ContactEmail_Value, PhoneNumber_Value, AvatarUrl, Website, Bio, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('d6ad1aae-2301-4dcd-b54e-637fe068d7c4', 'fa72fce6-e9e4-4dbf-a8fe-3bd972a2a1f7', 'ORG001', 'FPT Organizer', 'organizer@example.com', '0123456789', 'https://example.com/avatar.png', 'https://organizer.example.com', 'This is a test organizer', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);

-- Insert Event
INSERT INTO Event (Id, OrganizerId, SubCategoryId, VenueId, ImageUrl, Code, Title, Description, Address_Street, Address_City, Address_Country, Address_ZipCode, TotalTickets, TotalTicketsAvailable, StartTime, EndTime, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('81DF4DD8-780D-44B7-92B1-4D26B8D01994', 'd6ad1aae-2301-4dcd-b54e-637fe068d7c4', 'c19bc5b4-aea3-498c-9c1f-704b03bb14af', null, 'https://example.com/images/event1.png', 'EVT001', 'Rock Concert Night', 'An electrifying rock night with top bands.', '123 Music Street', 'Hanoi', 'Vietnam', '100000', 500, 500, '2025-06-01T19:00:00+07:00', '2025-06-01T22:00:00+07:00', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);
INSERT INTO Event (Id, OrganizerId, SubCategoryId, VenueId, ImageUrl, Code, Title, Description, Address_Street, Address_City, Address_Country, Address_ZipCode, TotalTickets, TotalTicketsAvailable, StartTime, EndTime, CreatedAt, LastModifiedAt, DeleteAt, DeleteFlag)
VALUES ('51AEC739-F39C-4E01-8B64-F82C522F8BAA', 'd6ad1aae-2301-4dcd-b54e-637fe068d7c4', 'd9180fff-5932-489f-907d-79ee4f5f93d7', null, 'https://example.com/images/event2.png', 'EVT002', 'Math Olympiad', 'A competition for brilliant math minds.', '456 Education Avenue', 'Da Nang', 'Vietnam', '550000', 200, 200, '2025-07-10T08:00:00+07:00', '2025-07-10T12:00:00+07:00', SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), SYSDATETIMEOFFSET(), 0);


