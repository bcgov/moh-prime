db.createUser(
  {
    user: "prime",
    pwd: "prime",
    roles: [ 
      { 
        role: "userAdminAnyDatabase",
        db: "admin" 
      }, 
      "readWriteAnyDatabase" 
    ]
  }
);
