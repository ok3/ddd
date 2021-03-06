﻿using System;
using System.Collections.Generic;
using SnsDomain.Models.Users;

namespace SnsDomain.Models.Circles
{
    public class Circle
    {
        public Circle(CircleId id, CircleName name, UserId owner, List<UserId> members)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (members == null) throw new ArgumentNullException(nameof(members));

            Id = id;
            Name = name;
            Owner = owner;
            Members = members;
        }

        public CircleId Id { get; }
        public CircleName Name { get; private set; }
        public UserId Owner { get; private set; }
        public List<UserId> Members { get; }

        public void Join(User member, CircleFullSpecification fullSpec)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (fullSpec.IsSatisfiedBy(this)) throw new CircleFullException(Id, "サークルに所属しているメンバーが上限に達しています。");

            Members.Add(member.Id);
        }

        public int CountMembers()
        {
            if (ExistsOwner())
            {
                return Members.Count + 1;
            }
            else
            {
                return Members.Count;
            }
        }

        public void ChangeName(CircleName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public List<UserId> GetMembers(bool containsOwner = true)
        {
            var members = new List<UserId>();
            if (containsOwner && ExistsOwner())
            {
                members.Add(Owner);
            }
            members.AddRange(Members);

            return members;
        }

        public bool ExistsOwner()
        {
            return Owner != null;
        }
    }
}
