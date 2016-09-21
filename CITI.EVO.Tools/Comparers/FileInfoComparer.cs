﻿using System;
using System.IO;

namespace CITI.EVO.Tools.Comparers
{
    public class FileInfoComparer : ComparerBase<FileInfo>
    {
        private static readonly StringComparer nameComparer = StringComparer.Ordinal;

        public override int Compare(FileInfo x, FileInfo y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x == null && y != null)
            {
                return -1;
            }

            if (x != null && y == null)
            {
                return 1;
            }

            var order = nameComparer.Compare(x.FullName, y.FullName);
            return order;
        }

        public override bool Equals(FileInfo x, FileInfo y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public override int GetHashCode(FileInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return nameComparer.GetHashCode(obj.FullName);
        }
    }
}