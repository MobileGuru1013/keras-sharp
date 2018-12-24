﻿// Keras-Sharp: C# port of the Keras library
// https://github.com/cesarsouza/keras-sharp
//
// Based under the Keras library for Python. See LICENSE text for more details.
//
//    The MIT License(MIT)
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//    
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
//

namespace KerasSharp.Engine.Topology
{
    using KerasSharp.Backends;
    using KerasSharp.Layers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using static KerasSharp.Python;
    using System.Diagnostics;
    using CNTK;

    [DataContract]
    public class CNTKTensor : Tensor
    {
        public Function function;
        public Parameter parameter;

        public new CNTKBackend K
        {
            get { return base.K as CNTKBackend; }
        }

        public CNTKTensor(IBackend backend)
            : base(backend)
        {
        }

        public new DataType dtype
        {
            get { return function.Output.DataType; }
        }


        public NDShape CNTK_Shape
        {
            get { return function.Output.Shape; }
        }

        public override string name
        {
            get
            {
                return function.Name;
            }
        }

        public static implicit operator CNTK.Variable(CNTKTensor t)
        {
            return t.function;
        }

        public static implicit operator CNTK.Function(CNTKTensor t)
        {
            return t.function;
        }



        public override string ToString()
        {
            string uid = function.Uid;
            string s = str(shape);
            string r = $"KerasSharp.Engine.Topology.Tensor '{uid}' shape={s} dtype={function.Output.DataType}";
            return r;
        }
    }
}
