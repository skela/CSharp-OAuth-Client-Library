﻿#region Copyright
// Copyright (C) 2012 Mathieu Turcotte
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System.Net;
using OAuth.Base;

namespace OAuth.Authenticator
{
    internal class HmacSha1RequestAuthenticator : OAuthRequestAuthenticator
    {
        public HmacSha1RequestAuthenticator(ClientCredentials credentials, AccessToken token) :
            base(credentials, token)
        {
        }

        protected override Signature GenerateSignature(WebRequest request, Nonce nonce, TimeStamp timestamp)
        {
            BaseString baseString = new BaseString(request.RequestUri, request.Method,
                nonce, timestamp, credentials, HmacSha1Signature.MethodName);
            baseString.Token = token;
			baseString.VerifierCode = token.Verifier;

            return new HmacSha1Signature(baseString.ToString(), credentials, token);
        }
    }
}
