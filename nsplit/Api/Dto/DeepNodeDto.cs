﻿// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Runtime.Serialization;

#endregion

namespace nsplit.Api.Dto
{
    [DataContract]
    public class DeepNodeDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "text")]
        public string Name { get; set; }

        [DataMember(Name = "children")]
        public DeepNodeDto[] Children { get; set; }

        [IgnoreDataMember]
        public bool IsLeaf { get; set; }

        [DataMember(Name = "icon")]
        public string Icon
        {
            get { return IsLeaf ? "/css/c.png" : "/css/n.png"; }
        }
    }
}