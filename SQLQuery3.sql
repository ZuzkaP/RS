CREATE TABLE users_roles (
	users_roles_id int,
	users_id int,
	roles_id int,
	FOREIGN KEY(users_id) REFERENCES Users (user_id),
	FOREIGN KEY(roles_id) REFERENCES Roles (Id),
	PRIMARY KEY CLUSTERED ([users_roles_id] ASC)
);