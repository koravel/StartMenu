#region Assembly Microsoft.Speech, Version=11.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Program Files\Microsoft SDKs\Speech\v11.0\Assembly\Microsoft.Speech.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Speech.Internal.SapiInterop;
using Microsoft.Win32;

namespace SpeechRecognitionTest;

internal class ObjectTokenCategory : RegistryDataKey, IEnumerable<ObjectToken>, IEnumerable
{
    protected ObjectTokenCategory(string keyId, RegistryKey hkey)
        : base(keyId, hkey)
    {
    }

    protected ObjectTokenCategory(string keyId, RegistryDataKey key)
        : base(keyId, key)
    {
    }

    internal static ObjectTokenCategory Create(string sCategoryId)
    {
        RegistryDataKey key = RegistryDataKey.Open(sCategoryId, fCreateIfNotExist: true);
        return new ObjectTokenCategory(sCategoryId, key);
    }

    internal ObjectToken OpenToken(string keyName)
    {
        string text = keyName;
        if (!string.IsNullOrEmpty(text) && text.IndexOf("HKEY_", StringComparison.Ordinal) != 0)
        {
            text = string.Format(CultureInfo.InvariantCulture, "{0}\\Tokens\\{1}", new object[2] { base.Id, text });
        }

        bool flag = false;
        if (!string.IsNullOrEmpty(text) && (text.IndexOf("Voices\\Tokens", StringComparison.Ordinal) > 0 || text.IndexOf("Voices\\TokenEnums", StringComparison.Ordinal) > 0))
        {
            flag = true;
        }

        if (!flag)
        {
            return ObjectToken.Open(null, text, fCreateIfNotExist: false);
        }

        return VoiceObjectToken.Create(null, text);
    }

    internal IList<ObjectToken> FindMatchingTokens(string requiredAttributes, string optionalAttributes)
    {
        IList<ObjectToken> list = new List<ObjectToken>();
        ISpObjectTokenCategory spObjectTokenCategory = null;
        IEnumSpObjectTokens ppEnum = null;
        try
        {
            spObjectTokenCategory = (ISpObjectTokenCategory)new SpObjectTokenCategory();
            spObjectTokenCategory.SetId(_sKeyId, fCreateIfNotExist: false);
            spObjectTokenCategory.EnumTokens(requiredAttributes, optionalAttributes, out ppEnum);
            ppEnum.GetCount(out var pCount);
            for (uint num = 0u; num < pCount; num++)
            {
                ISpObjectToken ppToken = null;
                IntPtr ppszCoMemTokenId = IntPtr.Zero;
                try
                {
                    ppEnum.Item(num, out ppToken);
                    ppToken.GetId(out ppszCoMemTokenId);
                    string sTokenId = Marshal.PtrToStringUni(ppszCoMemTokenId);
                    ObjectToken item = ObjectToken.Open(null, sTokenId, fCreateIfNotExist: false);
                    list.Add(item);
                }
                finally
                {
                    Marshal.FreeCoTaskMem(ppszCoMemTokenId);
                    if (ppToken != null)
                    {
                        Marshal.ReleaseComObject(ppToken);
                    }
                }
            }

            return list;
        }
        finally
        {
            if (ppEnum != null)
            {
                Marshal.ReleaseComObject(ppEnum);
            }

            if (spObjectTokenCategory != null)
            {
                Marshal.ReleaseComObject(spObjectTokenCategory);
            }
        }
    }

    IEnumerator<ObjectToken> IEnumerable<ObjectToken>.GetEnumerator()
    {
        if (TryOpenKey("Tokens", out var token))
        {
            foreach (RegistryDataKey key2 in (IEnumerable<RegistryDataKey>)token)
            {
                _ = key2.Id;
                yield return OpenToken(key2.Id);
                key2.Dispose();
            }

            token.Dispose();
        }

        if (!TryOpenKey("TokenEnums", out token))
        {
            yield break;
        }

        foreach (RegistryDataKey key in (IEnumerable<RegistryDataKey>)token)
        {
            _ = key.Id;
            yield return OpenToken(key.Id);
            key.Dispose();
        }

        token.Dispose();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<ObjectToken>)this).GetEnumerator();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}