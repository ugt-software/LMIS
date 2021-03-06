﻿using System;
using System.Runtime.Serialization;

namespace CITI.EVO.UserManagement.Svc.Contracts
{
    [Serializable]
    [DataContract]
    public class GroupContract
    {
        public GroupContract()
        {
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public Guid? ParentID { get; set; }

        [DataMember]
        public Guid ProjectID { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public int AccessLevel { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateChanged { get; set; }

        [DataMember]
        public DateTime? DateDeleted { get; set; }

    }
}