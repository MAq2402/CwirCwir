using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Comparers
{
    public class PostDateComparer : IComparer<Post>
    {
        public int Compare(Post x, Post y)
        {
            return -x.PostDate.CompareTo(y.PostDate);
        }
    }
    public class ResponseDateComparer : IComparer<Response>
    {
        public int Compare(Response x, Response y)
        {
            return -x.PostDate.CompareTo(y.PostDate);
        }
    }
}
