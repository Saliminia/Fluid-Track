using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace DMR
{
    public class UserRole
    {
        const string CLASS_NAME = "UserRole";
        //=======================================================
        public string userID = "";
        public RoleAccess accessParams = new RoleAccess();
        //=======================================================
        public void LoadAccessFromDB(string id)
        {
            userID = id;

            accessParams.FillFromRole(UserOperations.UserRole(id));
        }
        //-------------------------------------------------------
    }
}
