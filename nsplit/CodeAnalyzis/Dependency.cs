﻿// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace nsplit.CodeAnalyzis
{
    public abstract class Dependency
    {
        private readonly Type m_Source;
        private readonly Type m_Target;

        protected Dependency(Type source, Type target)
        {
            m_Source = Unnest(source);
            m_Target = Unnest(target);
        }

        public Type Target
        {
            get { return m_Target; }
        }

        public Type Source
        {
            get { return m_Source; }
        }

        public abstract DependencyKind Kind { get;}

        protected static Type Unnest(Type type)
        {
            if (type.IsNestedPrivate && type.DeclaringType != null) return Unnest(type.DeclaringType);
            return type;
        }

        public override int GetHashCode()
        {
            return m_Source.GetHashCode() ^ m_Target.GetHashCode() ^ Kind.GetHashCode();
        }

        public override bool Equals(object other)
        {
            var otherDependency = other as Dependency;
            return otherDependency != null &&
                   otherDependency.m_Source == m_Source &&
                   otherDependency.m_Target == m_Target &&
                   otherDependency.Kind == Kind;
        }

        public override string ToString()
        {
            return string.Format("{0,20} => {1,-20}", Source.Name, Target.Name);
        }
    }
}