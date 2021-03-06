﻿using Microsoft.AspNet.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.AspNet.Identity.DomainModel;
using System.Collections.Generic;

namespace NHibernate.AspNet.Identity
{
    public class IdentityRole : EntityWithTypedId<string>, IRole
    {
        public virtual string Name { get; set; }

        public virtual ICollection<IdentityUser> Users { get; protected set; }

        public IdentityRole()
        {
            this.Users = (ICollection<IdentityUser>)new List<IdentityUser>();
        }

        public IdentityRole(string roleName)
            : this()
        {
            this.Name = roleName;
        }
    }

    public class IdentityRoleMap : ClassMapping<IdentityRole> 
    {
        public IdentityRoleMap()
        {
            Table("AspNetRoles");
            Id(x => x.Id, m => m.Generator(new UUIDHexCombGeneratorDef("D")));
            Property(x => x.Name, m => m.NotNullable(false));
            Bag(x => x.Users, map => {
                map.Table("AspNetUserRoles");
                map.Cascade(Cascade.None);
                map.Key(k => k.Column("RoleId"));
            }, rel => rel.ManyToMany(p => p.Column("UserId")));
        }
    }
}
