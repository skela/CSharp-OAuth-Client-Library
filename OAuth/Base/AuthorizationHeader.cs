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

using System;

namespace OAuth.Base
{
    internal class AuthorizationHeader
    {
        private readonly ClientCredentials credentials;
        private readonly TimeStamp timestamp;
        private readonly Nonce nonce;
        private readonly Signature signature;

        private Token token;
        private string callback;
        private string verifier;

        public AuthorizationHeader(ClientCredentials credentials, Nonce nonce, TimeStamp timestamp, Signature signature)
        {
            this.credentials = credentials;
            this.timestamp = timestamp;
            this.nonce = nonce;
            this.signature = signature;
        }

        public Token Token { set { token = value; } }
        public string CallbackUrl { set { callback = value; } }
        public string VerifierCode { set { verifier = value; } }

        public override string ToString()
        {
            AuthorizationHeaderBuilder header = new AuthorizationHeaderBuilder();

            header.Append("OAuth ");
			if (credentials.Realm!=null && credentials.Realm.Length>0)
				header.AppendField(AuthorizationHeaderFields.REALM,credentials.Realm);
            header.AppendField(AuthorizationHeaderFields.VERSION, OAuthVersion.VERSION);
            header.AppendField(AuthorizationHeaderFields.CONSUMER_KEY, credentials.Identifier);
            header.AppendField(AuthorizationHeaderFields.SIGNATURE_METHOD, signature.Method);

			if (token != null && !String.IsNullOrEmpty(token.Value))
            {
                header.AppendField(AuthorizationHeaderFields.TOKEN, token.Value);
            }

            if (!String.IsNullOrEmpty(verifier))
            {
                header.AppendField(AuthorizationHeaderFields.VERIFIER, verifier);
            }

            if (!String.IsNullOrEmpty(callback))
            {
                header.AppendField(AuthorizationHeaderFields.CALLBACK, callback);
            }

            header.AppendField(AuthorizationHeaderFields.NONCE, nonce.ToString());
            header.AppendField(AuthorizationHeaderFields.TIMESTAMP, timestamp.ToString());
            header.AppendField(AuthorizationHeaderFields.SIGNATURE, signature.Value);

			String val = header.ToString ();

			return val;
        }
    }
}
