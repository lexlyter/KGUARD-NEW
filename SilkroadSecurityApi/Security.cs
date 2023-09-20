// Decompiled with JetBrains decompiler
// Type: SilkroadSecurityApi.Security
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SilkroadSecurityApi
{
  public class Security
  {
    private static uint[] global_security_table = Security.GenerateSecurityTable();
    private static Random random = new Random();
    private uint m_value_x;
    private uint m_value_g;
    private uint m_value_p;
    private uint m_value_A;
    private uint m_value_B;
    private uint m_value_K;
    private uint m_seed_count;
    private uint m_crc_seed;
    private ulong m_initial_blowfish_key;
    private ulong m_handshake_blowfish_key;
    private byte[] m_count_byte_seeds;
    private ulong m_client_key;
    private ulong m_challenge_key;
    private bool m_client_security;
    private byte m_security_flag;
    private Security.SecurityFlags m_security_flags;
    private bool m_accepted_handshake;
    private bool m_started_handshake;
    private byte m_identity_flag;
    private string m_identity_name;
    private List<Packet> m_incoming_packets;
    private List<Packet> m_outgoing_packets;
    private List<ushort> m_enc_opcodes;
    private Blowfish m_blowfish;
    private TransferBuffer m_recv_buffer;
    private TransferBuffer m_current_buffer;
    private ushort m_massive_count;
    private Packet m_massive_packet;
    private object m_class_lock;

    private static Security.SecurityFlags CopySecurityFlags(Security.SecurityFlags flags) => new Security.SecurityFlags()
    {
      none = flags.none,
      blowfish = flags.blowfish,
      security_bytes = flags.security_bytes,
      handshake = flags.handshake,
      handshake_response = flags.handshake_response,
      _6 = flags._6,
      _7 = flags._7,
      _8 = flags._8
    };

    private static byte FromSecurityFlags(Security.SecurityFlags flags) => (byte) ((int) flags.none | (int) flags.blowfish << 1 | (int) flags.security_bytes << 2 | (int) flags.handshake << 3 | (int) flags.handshake_response << 4 | (int) flags._6 << 5 | (int) flags._7 << 6 | (int) flags._8 << 7);

    private static Security.SecurityFlags ToSecurityFlags(byte value)
    {
      Security.SecurityFlags securityFlags = new Security.SecurityFlags();
      securityFlags.none = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags.blowfish = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags.security_bytes = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags.handshake = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags.handshake_response = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags._6 = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags._7 = (byte) ((uint) value & 1U);
      value >>= 1;
      securityFlags._8 = (byte) ((uint) value & 1U);
      value >>= 1;
      return securityFlags;
    }

    private static uint[] GenerateSecurityTable()
    {
      uint[] numArray = new uint[65536];
      using (MemoryStream memoryStream = new MemoryStream(new byte[1024]
      {
        (byte) 177,
        (byte) 214,
        (byte) 139,
        (byte) 150,
        (byte) 150,
        (byte) 48,
        (byte) 7,
        (byte) 119,
        (byte) 44,
        (byte) 97,
        (byte) 14,
        (byte) 238,
        (byte) 186,
        (byte) 81,
        (byte) 9,
        (byte) 153,
        (byte) 25,
        (byte) 196,
        (byte) 109,
        (byte) 7,
        (byte) 143,
        (byte) 244,
        (byte) 106,
        (byte) 112,
        (byte) 53,
        (byte) 165,
        (byte) 99,
        (byte) 233,
        (byte) 163,
        (byte) 149,
        (byte) 100,
        (byte) 158,
        (byte) 50,
        (byte) 136,
        (byte) 219,
        (byte) 14,
        (byte) 164,
        (byte) 184,
        (byte) 220,
        (byte) 121,
        (byte) 30,
        (byte) 233,
        (byte) 213,
        (byte) 224,
        (byte) 136,
        (byte) 217,
        (byte) 210,
        (byte) 151,
        (byte) 43,
        (byte) 76,
        (byte) 182,
        (byte) 9,
        (byte) 189,
        (byte) 124,
        (byte) 177,
        (byte) 126,
        (byte) 7,
        (byte) 45,
        (byte) 184,
        (byte) 231,
        (byte) 145,
        (byte) 29,
        (byte) 191,
        (byte) 144,
        (byte) 100,
        (byte) 16,
        (byte) 183,
        (byte) 29,
        (byte) 242,
        (byte) 32,
        (byte) 176,
        (byte) 106,
        (byte) 72,
        (byte) 113,
        (byte) 177,
        (byte) 243,
        (byte) 222,
        (byte) 65,
        (byte) 190,
        (byte) 140,
        (byte) 125,
        (byte) 212,
        (byte) 218,
        (byte) 26,
        (byte) 235,
        (byte) 228,
        (byte) 221,
        (byte) 109,
        (byte) 81,
        (byte) 181,
        (byte) 212,
        (byte) 244,
        (byte) 199,
        (byte) 133,
        (byte) 211,
        (byte) 131,
        (byte) 86,
        (byte) 152,
        (byte) 108,
        (byte) 19,
        (byte) 192,
        (byte) 168,
        (byte) 107,
        (byte) 100,
        (byte) 122,
        (byte) 249,
        (byte) 98,
        (byte) 253,
        (byte) 236,
        (byte) 201,
        (byte) 101,
        (byte) 138,
        (byte) 79,
        (byte) 92,
        (byte) 1,
        (byte) 20,
        (byte) 217,
        (byte) 108,
        (byte) 6,
        (byte) 99,
        (byte) 99,
        (byte) 61,
        (byte) 15,
        (byte) 250,
        (byte) 245,
        (byte) 13,
        (byte) 8,
        (byte) 141,
        (byte) 200,
        (byte) 32,
        (byte) 110,
        (byte) 59,
        (byte) 94,
        (byte) 16,
        (byte) 105,
        (byte) 76,
        (byte) 228,
        (byte) 65,
        (byte) 96,
        (byte) 213,
        (byte) 114,
        (byte) 113,
        (byte) 103,
        (byte) 162,
        (byte) 209,
        (byte) 228,
        (byte) 3,
        (byte) 60,
        (byte) 71,
        (byte) 212,
        (byte) 4,
        (byte) 75,
        (byte) 253,
        (byte) 133,
        (byte) 13,
        (byte) 210,
        (byte) 107,
        (byte) 181,
        (byte) 10,
        (byte) 165,
        (byte) 250,
        (byte) 168,
        (byte) 181,
        (byte) 53,
        (byte) 108,
        (byte) 152,
        (byte) 178,
        (byte) 66,
        (byte) 214,
        (byte) 201,
        (byte) 187,
        (byte) 219,
        (byte) 64,
        (byte) 249,
        (byte) 188,
        (byte) 172,
        (byte) 227,
        (byte) 108,
        (byte) 216,
        (byte) 50,
        (byte) 117,
        (byte) 92,
        (byte) 223,
        (byte) 69,
        (byte) 207,
        (byte) 13,
        (byte) 214,
        (byte) 220,
        (byte) 89,
        (byte) 61,
        (byte) 209,
        (byte) 171,
        (byte) 172,
        (byte) 48,
        (byte) 217,
        (byte) 38,
        (byte) 58,
        (byte) 0,
        (byte) 222,
        (byte) 81,
        (byte) 128,
        (byte) 81,
        (byte) 215,
        (byte) 200,
        (byte) 22,
        (byte) 97,
        (byte) 208,
        (byte) 191,
        (byte) 181,
        (byte) 244,
        (byte) 180,
        (byte) 33,
        (byte) 35,
        (byte) 196,
        (byte) 179,
        (byte) 86,
        (byte) 153,
        (byte) 149,
        (byte) 186,
        (byte) 207,
        (byte) 15,
        (byte) 165,
        (byte) 183,
        (byte) 184,
        (byte) 158,
        (byte) 184,
        (byte) 2,
        (byte) 40,
        (byte) 8,
        (byte) 136,
        (byte) 5,
        (byte) 95,
        (byte) 178,
        (byte) 217,
        (byte) 236,
        (byte) 198,
        (byte) 36,
        (byte) 233,
        (byte) 11,
        (byte) 177,
        (byte) 135,
        (byte) 124,
        (byte) 111,
        (byte) 47,
        (byte) 17,
        (byte) 76,
        (byte) 104,
        (byte) 88,
        (byte) 171,
        (byte) 29,
        (byte) 97,
        (byte) 193,
        (byte) 61,
        (byte) 45,
        (byte) 102,
        (byte) 182,
        (byte) 144,
        (byte) 65,
        (byte) 220,
        (byte) 118,
        (byte) 6,
        (byte) 113,
        (byte) 219,
        (byte) 1,
        (byte) 188,
        (byte) 32,
        (byte) 210,
        (byte) 152,
        (byte) 42,
        (byte) 16,
        (byte) 213,
        (byte) 239,
        (byte) 137,
        (byte) 133,
        (byte) 177,
        (byte) 113,
        (byte) 31,
        (byte) 181,
        (byte) 182,
        (byte) 6,
        (byte) 165,
        (byte) 228,
        (byte) 191,
        (byte) 159,
        (byte) 51,
        (byte) 212,
        (byte) 184,
        (byte) 232,
        (byte) 162,
        (byte) 201,
        (byte) 7,
        (byte) 120,
        (byte) 52,
        (byte) 249,
        (byte) 160,
        (byte) 15,
        (byte) 142,
        (byte) 168,
        (byte) 9,
        (byte) 150,
        (byte) 24,
        (byte) 152,
        (byte) 14,
        (byte) 225,
        (byte) 187,
        (byte) 13,
        (byte) 106,
        (byte) 127,
        (byte) 45,
        (byte) 61,
        (byte) 109,
        (byte) 8,
        (byte) 151,
        (byte) 108,
        (byte) 100,
        (byte) 145,
        (byte) 1,
        (byte) 92,
        (byte) 99,
        (byte) 230,
        (byte) 244,
        (byte) 81,
        (byte) 107,
        (byte) 107,
        (byte) 98,
        (byte) 97,
        (byte) 108,
        (byte) 28,
        (byte) 216,
        (byte) 48,
        (byte) 101,
        (byte) 133,
        (byte) 78,
        (byte) 0,
        (byte) 98,
        (byte) 242,
        (byte) 237,
        (byte) 149,
        (byte) 6,
        (byte) 108,
        (byte) 123,
        (byte) 165,
        (byte) 1,
        (byte) 27,
        (byte) 193,
        (byte) 244,
        (byte) 8,
        (byte) 130,
        (byte) 87,
        (byte) 196,
        (byte) 15,
        (byte) 245,
        (byte) 198,
        (byte) 217,
        (byte) 176,
        (byte) 99,
        (byte) 80,
        (byte) 233,
        (byte) 183,
        (byte) 18,
        (byte) 234,
        (byte) 184,
        (byte) 190,
        (byte) 139,
        (byte) 124,
        (byte) 136,
        (byte) 185,
        (byte) 252,
        (byte) 223,
        (byte) 29,
        (byte) 221,
        (byte) 98,
        (byte) 73,
        (byte) 45,
        (byte) 218,
        (byte) 21,
        (byte) 243,
        (byte) 124,
        (byte) 211,
        (byte) 140,
        (byte) 101,
        (byte) 76,
        (byte) 212,
        (byte) 251,
        (byte) 88,
        (byte) 97,
        (byte) 178,
        (byte) 77,
        (byte) 206,
        (byte) 81,
        (byte) 181,
        (byte) 58,
        (byte) 116,
        (byte) 0,
        (byte) 188,
        (byte) 163,
        (byte) 226,
        (byte) 48,
        (byte) 187,
        (byte) 212,
        (byte) 65,
        (byte) 165,
        (byte) 223,
        (byte) 74,
        (byte) 215,
        (byte) 149,
        (byte) 216,
        (byte) 61,
        (byte) 109,
        (byte) 196,
        (byte) 209,
        (byte) 164,
        (byte) 251,
        (byte) 244,
        (byte) 214,
        (byte) 211,
        (byte) 106,
        (byte) 233,
        (byte) 105,
        (byte) 67,
        (byte) 252,
        (byte) 217,
        (byte) 110,
        (byte) 52,
        (byte) 70,
        (byte) 136,
        (byte) 103,
        (byte) 173,
        (byte) 208,
        (byte) 184,
        (byte) 96,
        (byte) 218,
        (byte) 115,
        (byte) 45,
        (byte) 4,
        (byte) 68,
        (byte) 229,
        (byte) 29,
        (byte) 3,
        (byte) 51,
        (byte) 95,
        (byte) 76,
        (byte) 10,
        (byte) 170,
        (byte) 201,
        (byte) 124,
        (byte) 13,
        (byte) 221,
        (byte) 60,
        (byte) 113,
        (byte) 5,
        (byte) 80,
        (byte) 170,
        (byte) 65,
        (byte) 2,
        (byte) 39,
        (byte) 16,
        (byte) 16,
        (byte) 11,
        (byte) 190,
        (byte) 134,
        (byte) 32,
        (byte) 12,
        (byte) 201,
        (byte) 37,
        (byte) 181,
        (byte) 104,
        (byte) 87,
        (byte) 179,
        (byte) 133,
        (byte) 111,
        (byte) 32,
        (byte) 9,
        (byte) 212,
        (byte) 102,
        (byte) 185,
        (byte) 159,
        (byte) 228,
        (byte) 97,
        (byte) 206,
        (byte) 14,
        (byte) 249,
        (byte) 222,
        (byte) 94,
        (byte) 8,
        (byte) 201,
        (byte) 217,
        (byte) 41,
        (byte) 34,
        (byte) 152,
        (byte) 208,
        (byte) 176,
        (byte) 180,
        (byte) 168,
        (byte) 87,
        (byte) 199,
        (byte) 23,
        (byte) 61,
        (byte) 179,
        (byte) 89,
        (byte) 129,
        (byte) 13,
        (byte) 180,
        (byte) 62,
        (byte) 59,
        (byte) 92,
        (byte) 189,
        (byte) 183,
        (byte) 173,
        (byte) 108,
        (byte) 186,
        (byte) 192,
        (byte) 32,
        (byte) 131,
        (byte) 184,
        (byte) 237,
        (byte) 182,
        (byte) 179,
        (byte) 191,
        (byte) 154,
        (byte) 12,
        (byte) 226,
        (byte) 182,
        (byte) 3,
        (byte) 154,
        (byte) 210,
        (byte) 177,
        (byte) 116,
        (byte) 57,
        (byte) 71,
        (byte) 213,
        (byte) 234,
        (byte) 175,
        (byte) 119,
        (byte) 210,
        (byte) 157,
        (byte) 21,
        (byte) 38,
        (byte) 219,
        (byte) 4,
        (byte) 131,
        (byte) 22,
        (byte) 220,
        (byte) 115,
        (byte) 18,
        (byte) 11,
        (byte) 99,
        (byte) 227,
        (byte) 132,
        (byte) 59,
        (byte) 100,
        (byte) 148,
        (byte) 62,
        (byte) 106,
        (byte) 109,
        (byte) 13,
        (byte) 168,
        (byte) 90,
        (byte) 106,
        (byte) 122,
        (byte) 11,
        (byte) 207,
        (byte) 14,
        (byte) 228,
        (byte) 157,
        byte.MaxValue,
        (byte) 9,
        (byte) 147,
        (byte) 39,
        (byte) 174,
        (byte) 0,
        (byte) 10,
        (byte) 177,
        (byte) 158,
        (byte) 7,
        (byte) 125,
        (byte) 68,
        (byte) 147,
        (byte) 15,
        (byte) 240,
        (byte) 210,
        (byte) 162,
        (byte) 8,
        (byte) 135,
        (byte) 104,
        (byte) 242,
        (byte) 1,
        (byte) 30,
        (byte) 254,
        (byte) 194,
        (byte) 6,
        (byte) 105,
        (byte) 93,
        (byte) 87,
        (byte) 98,
        (byte) 247,
        (byte) 203,
        (byte) 103,
        (byte) 101,
        (byte) 128,
        (byte) 113,
        (byte) 54,
        (byte) 108,
        (byte) 25,
        (byte) 231,
        (byte) 6,
        (byte) 107,
        (byte) 110,
        (byte) 118,
        (byte) 27,
        (byte) 212,
        (byte) 254,
        (byte) 224,
        (byte) 43,
        (byte) 211,
        (byte) 137,
        (byte) 90,
        (byte) 122,
        (byte) 218,
        (byte) 16,
        (byte) 204,
        (byte) 74,
        (byte) 221,
        (byte) 103,
        (byte) 111,
        (byte) 223,
        (byte) 185,
        (byte) 249,
        (byte) 249,
        (byte) 239,
        (byte) 190,
        (byte) 142,
        (byte) 67,
        (byte) 190,
        (byte) 183,
        (byte) 23,
        (byte) 213,
        (byte) 142,
        (byte) 176,
        (byte) 96,
        (byte) 232,
        (byte) 163,
        (byte) 214,
        (byte) 214,
        (byte) 126,
        (byte) 147,
        (byte) 209,
        (byte) 161,
        (byte) 196,
        (byte) 194,
        (byte) 216,
        (byte) 56,
        (byte) 82,
        (byte) 242,
        (byte) 223,
        (byte) 79,
        (byte) 241,
        (byte) 103,
        (byte) 187,
        (byte) 209,
        (byte) 103,
        (byte) 87,
        (byte) 188,
        (byte) 166,
        (byte) 221,
        (byte) 6,
        (byte) 181,
        (byte) 63,
        (byte) 75,
        (byte) 54,
        (byte) 178,
        (byte) 72,
        (byte) 218,
        (byte) 43,
        (byte) 13,
        (byte) 216,
        (byte) 76,
        (byte) 27,
        (byte) 10,
        (byte) 175,
        (byte) 246,
        (byte) 74,
        (byte) 3,
        (byte) 54,
        (byte) 96,
        (byte) 122,
        (byte) 4,
        (byte) 65,
        (byte) 195,
        (byte) 239,
        (byte) 96,
        (byte) 223,
        (byte) 85,
        (byte) 223,
        (byte) 103,
        (byte) 168,
        (byte) 239,
        (byte) 142,
        (byte) 110,
        (byte) 49,
        (byte) 121,
        (byte) 14,
        (byte) 105,
        (byte) 70,
        (byte) 140,
        (byte) 179,
        (byte) 81,
        (byte) 203,
        (byte) 26,
        (byte) 131,
        (byte) 99,
        (byte) 188,
        (byte) 160,
        (byte) 210,
        (byte) 111,
        (byte) 37,
        (byte) 54,
        (byte) 226,
        (byte) 104,
        (byte) 82,
        (byte) 149,
        (byte) 119,
        (byte) 12,
        (byte) 204,
        (byte) 3,
        (byte) 71,
        (byte) 11,
        (byte) 187,
        (byte) 185,
        (byte) 20,
        (byte) 2,
        (byte) 34,
        (byte) 47,
        (byte) 38,
        (byte) 5,
        (byte) 85,
        (byte) 190,
        (byte) 59,
        (byte) 182,
        (byte) 197,
        (byte) 40,
        (byte) 11,
        (byte) 189,
        (byte) 178,
        (byte) 146,
        (byte) 90,
        (byte) 180,
        (byte) 43,
        (byte) 4,
        (byte) 106,
        (byte) 179,
        (byte) 92,
        (byte) 167,
        byte.MaxValue,
        (byte) 215,
        (byte) 194,
        (byte) 49,
        (byte) 207,
        (byte) 208,
        (byte) 181,
        (byte) 139,
        (byte) 158,
        (byte) 217,
        (byte) 44,
        (byte) 29,
        (byte) 174,
        (byte) 222,
        (byte) 91,
        (byte) 176,
        (byte) 114,
        (byte) 100,
        (byte) 155,
        (byte) 38,
        (byte) 242,
        (byte) 227,
        (byte) 236,
        (byte) 156,
        (byte) 163,
        (byte) 106,
        (byte) 117,
        (byte) 10,
        (byte) 147,
        (byte) 109,
        (byte) 2,
        (byte) 169,
        (byte) 6,
        (byte) 9,
        (byte) 156,
        (byte) 63,
        (byte) 54,
        (byte) 14,
        (byte) 235,
        (byte) 133,
        (byte) 104,
        (byte) 7,
        (byte) 114,
        (byte) 19,
        (byte) 7,
        (byte) 0,
        (byte) 5,
        (byte) 130,
        (byte) 72,
        (byte) 191,
        (byte) 149,
        (byte) 20,
        (byte) 122,
        (byte) 184,
        (byte) 226,
        (byte) 174,
        (byte) 43,
        (byte) 177,
        (byte) 123,
        (byte) 56,
        (byte) 27,
        (byte) 182,
        (byte) 12,
        (byte) 155,
        (byte) 142,
        (byte) 210,
        (byte) 146,
        (byte) 13,
        (byte) 190,
        (byte) 213,
        (byte) 229,
        (byte) 183,
        (byte) 239,
        (byte) 220,
        (byte) 124,
        (byte) 33,
        (byte) 223,
        (byte) 219,
        (byte) 11,
        (byte) 148,
        (byte) 210,
        (byte) 211,
        (byte) 134,
        (byte) 66,
        (byte) 226,
        (byte) 212,
        (byte) 241,
        (byte) 248,
        (byte) 179,
        (byte) 221,
        (byte) 104,
        (byte) 110,
        (byte) 131,
        (byte) 218,
        (byte) 31,
        (byte) 205,
        (byte) 22,
        (byte) 190,
        (byte) 129,
        (byte) 91,
        (byte) 38,
        (byte) 185,
        (byte) 246,
        (byte) 225,
        (byte) 119,
        (byte) 176,
        (byte) 111,
        (byte) 119,
        (byte) 71,
        (byte) 183,
        (byte) 24,
        (byte) 224,
        (byte) 90,
        (byte) 8,
        (byte) 136,
        (byte) 112,
        (byte) 106,
        (byte) 15,
        (byte) 241,
        (byte) 202,
        (byte) 59,
        (byte) 6,
        (byte) 102,
        (byte) 92,
        (byte) 11,
        (byte) 1,
        (byte) 17,
        byte.MaxValue,
        (byte) 158,
        (byte) 101,
        (byte) 143,
        (byte) 105,
        (byte) 174,
        (byte) 98,
        (byte) 248,
        (byte) 211,
        byte.MaxValue,
        (byte) 107,
        (byte) 97,
        (byte) 69,
        (byte) 207,
        (byte) 108,
        (byte) 22,
        (byte) 120,
        (byte) 226,
        (byte) 10,
        (byte) 160,
        (byte) 238,
        (byte) 210,
        (byte) 13,
        (byte) 215,
        (byte) 84,
        (byte) 131,
        (byte) 4,
        (byte) 78,
        (byte) 194,
        (byte) 179,
        (byte) 3,
        (byte) 57,
        (byte) 97,
        (byte) 38,
        (byte) 103,
        (byte) 167,
        (byte) 247,
        (byte) 22,
        (byte) 96,
        (byte) 208,
        (byte) 77,
        (byte) 71,
        (byte) 105,
        (byte) 73,
        (byte) 219,
        (byte) 119,
        (byte) 110,
        (byte) 62,
        (byte) 74,
        (byte) 106,
        (byte) 209,
        (byte) 174,
        (byte) 220,
        (byte) 90,
        (byte) 214,
        (byte) 217,
        (byte) 102,
        (byte) 11,
        (byte) 223,
        (byte) 64,
        (byte) 240,
        (byte) 59,
        (byte) 216,
        (byte) 55,
        (byte) 83,
        (byte) 174,
        (byte) 188,
        (byte) 169,
        (byte) 197,
        (byte) 158,
        (byte) 187,
        (byte) 222,
        (byte) 127,
        (byte) 207,
        (byte) 178,
        (byte) 71,
        (byte) 233,
        byte.MaxValue,
        (byte) 181,
        (byte) 48,
        (byte) 28,
        (byte) 249,
        (byte) 189,
        (byte) 189,
        (byte) 138,
        (byte) 205,
        (byte) 186,
        (byte) 202,
        (byte) 48,
        (byte) 158,
        (byte) 179,
        (byte) 83,
        (byte) 166,
        (byte) 163,
        (byte) 188,
        (byte) 36,
        (byte) 5,
        (byte) 59,
        (byte) 208,
        (byte) 186,
        (byte) 163,
        (byte) 6,
        (byte) 215,
        (byte) 205,
        (byte) 233,
        (byte) 87,
        (byte) 222,
        (byte) 84,
        (byte) 191,
        (byte) 103,
        (byte) 217,
        (byte) 35,
        (byte) 46,
        (byte) 114,
        (byte) 102,
        (byte) 179,
        (byte) 184,
        (byte) 74,
        (byte) 97,
        (byte) 196,
        (byte) 2,
        (byte) 27,
        (byte) 56,
        (byte) 93,
        (byte) 148,
        (byte) 43,
        (byte) 111,
        (byte) 43,
        (byte) 55,
        (byte) 190,
        (byte) 203,
        (byte) 180,
        (byte) 161,
        (byte) 142,
        (byte) 204,
        (byte) 195,
        (byte) 27,
        (byte) 223,
        (byte) 13,
        (byte) 90,
        (byte) 141,
        (byte) 237,
        (byte) 2,
        (byte) 45
      }, false))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
        {
          int num1 = 0;
          for (int index1 = 0; index1 < 1024; index1 += 4)
          {
            uint num2 = binaryReader.ReadUInt32();
            for (uint index2 = 0; index2 < 256U; ++index2)
            {
              uint num3 = index2 >> 1;
              if ((index2 & 1U) > 0U)
                num3 ^= num2;
              for (int index3 = 0; index3 < 7; ++index3)
              {
                if ((num3 & 1U) > 0U)
                  num3 = num3 >> 1 ^ num2;
                else
                  num3 >>= 1;
              }
              numArray[num1++] = num3;
            }
          }
        }
      }
      return numArray;
    }

    private static ulong MAKELONGLONG_(uint a, uint b)
    {
      ulong num = (ulong) a;
      return (ulong) b << 32 | num;
    }

    private static uint MAKELONG_(ushort a, ushort b)
    {
      uint num = (uint) a;
      return (uint) b << 16 | num;
    }

    private static ushort MAKEWORD_(byte a, byte b)
    {
      ushort num = (ushort) a;
      return (ushort) ((uint) (ushort) b << 8 | (uint) num);
    }

    private static ushort LOWORD_(uint a) => (ushort) (a & (uint) ushort.MaxValue);

    private static ushort HIWORD_(uint a) => (ushort) (a >> 16 & (uint) ushort.MaxValue);

    private static byte LOBYTE_(ushort a) => (byte) ((uint) a & (uint) byte.MaxValue);

    private static byte HIBYTE_(ushort a) => (byte) ((int) a >> 8 & (int) byte.MaxValue);

    private static ulong NextUInt64()
    {
      byte[] buffer = new byte[8];
      Security.random.NextBytes(buffer);
      return BitConverter.ToUInt64(buffer, 0);
    }

    private static uint NextUInt32()
    {
      byte[] buffer = new byte[4];
      Security.random.NextBytes(buffer);
      return BitConverter.ToUInt32(buffer, 0);
    }

    private static ushort NextUInt16()
    {
      byte[] buffer = new byte[2];
      Security.random.NextBytes(buffer);
      return BitConverter.ToUInt16(buffer, 0);
    }

    private static byte NextUInt8() => (byte) ((uint) Security.NextUInt16() & (uint) byte.MaxValue);

    private uint GenerateValue(ref uint val)
    {
      for (int index = 0; index < 32; ++index)
        val = (uint) (((int) (((((val >> 2 ^ val) >> 2 ^ val) >> 1 ^ val) >> 1 ^ val) >> 1) ^ (int) val) & 1 | (((int) val & 1) << 31 | (int) (val >> 1)) & -2);
      return val;
    }

    private void SetupCountByte(uint seed)
    {
      if (seed == 0U)
        seed = 2596254646U;
      uint val = seed;
      uint num1 = this.GenerateValue(ref val);
      uint num2 = this.GenerateValue(ref val);
      uint num3 = this.GenerateValue(ref val);
      int num4 = (int) this.GenerateValue(ref val);
      byte num5 = (byte) ((int) val & (int) byte.MaxValue ^ (int) num3 & (int) byte.MaxValue);
      byte num6 = (byte) ((int) num1 & (int) byte.MaxValue ^ (int) num2 & (int) byte.MaxValue);
      if (num5 == (byte) 0)
        num5 = (byte) 1;
      if (num6 == (byte) 0)
        num6 = (byte) 1;
      this.m_count_byte_seeds[0] = (byte) ((uint) num5 ^ (uint) num6);
      this.m_count_byte_seeds[1] = num6;
      this.m_count_byte_seeds[2] = num5;
    }

    private uint G_pow_X_mod_P(uint P, uint X, uint G)
    {
      long num1 = 1;
      long num2 = (long) G;
      if (X == 0U)
        return 1;
      while (X > 0U)
      {
        if ((X & 1U) > 0U)
          num1 = num2 * num1 % (long) P;
        X >>= 1;
        num2 = num2 * num2 % (long) P;
      }
      return (uint) num1;
    }

    private void KeyTransformValue(ref ulong val, uint key, byte key_byte)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      bytes[0] ^= (byte) ((uint) bytes[0] + (uint) Security.LOBYTE_(Security.LOWORD_(key)) + (uint) key_byte);
      bytes[1] ^= (byte) ((uint) bytes[1] + (uint) Security.HIBYTE_(Security.LOWORD_(key)) + (uint) key_byte);
      bytes[2] ^= (byte) ((uint) bytes[2] + (uint) Security.LOBYTE_(Security.HIWORD_(key)) + (uint) key_byte);
      bytes[3] ^= (byte) ((uint) bytes[3] + (uint) Security.HIBYTE_(Security.HIWORD_(key)) + (uint) key_byte);
      bytes[4] ^= (byte) ((uint) bytes[4] + (uint) Security.LOBYTE_(Security.LOWORD_(key)) + (uint) key_byte);
      bytes[5] ^= (byte) ((uint) bytes[5] + (uint) Security.HIBYTE_(Security.LOWORD_(key)) + (uint) key_byte);
      bytes[6] ^= (byte) ((uint) bytes[6] + (uint) Security.LOBYTE_(Security.HIWORD_(key)) + (uint) key_byte);
      bytes[7] ^= (byte) ((uint) bytes[7] + (uint) Security.HIBYTE_(Security.HIWORD_(key)) + (uint) key_byte);
      val = BitConverter.ToUInt64(bytes, 0);
    }

    private byte GenerateCountByte(bool update)
    {
      byte num1 = (byte) ((uint) this.m_count_byte_seeds[2] * ((uint) ~this.m_count_byte_seeds[0] + (uint) this.m_count_byte_seeds[1]));
      byte num2 = (byte) ((uint) num1 ^ (uint) num1 >> 4);
      if (update)
        this.m_count_byte_seeds[0] = num2;
      return num2;
    }

    private byte GenerateCheckByte(byte[] stream, int offset, int length)
    {
      uint num1 = uint.MaxValue;
      uint num2 = this.m_crc_seed << 8;
      for (int index = offset; index < offset + length; ++index)
        num1 = num1 >> 8 ^ Security.global_security_table[(int) num2 + (((int) stream[index] ^ (int) num1) & (int) byte.MaxValue)];
      return (byte) (((int) (num1 >> 24) & (int) byte.MaxValue) + ((int) (num1 >> 8) & (int) byte.MaxValue) + ((int) (num1 >> 16) & (int) byte.MaxValue) + ((int) num1 & (int) byte.MaxValue));
    }

    private byte GenerateCheckByte(byte[] stream) => this.GenerateCheckByte(stream, 0, stream.Length);

    private void GenerateSecurity(Security.SecurityFlags flags)
    {
      this.m_security_flag = Security.FromSecurityFlags(flags);
      this.m_security_flags = flags;
      this.m_client_security = true;
      Packet packet = new Packet((ushort) 20480);
      packet.WriteUInt8(this.m_security_flag);
      if (this.m_security_flags.blowfish == (byte) 1)
      {
        this.m_initial_blowfish_key = Security.NextUInt64();
        this.m_blowfish.Initialize(BitConverter.GetBytes(this.m_initial_blowfish_key));
        packet.WriteUInt64(this.m_initial_blowfish_key);
      }
      if (this.m_security_flags.security_bytes == (byte) 1)
      {
        this.m_seed_count = (uint) Security.NextUInt8();
        this.SetupCountByte(this.m_seed_count);
        this.m_crc_seed = (uint) Security.NextUInt8();
        packet.WriteUInt32(this.m_seed_count);
        packet.WriteUInt32(this.m_crc_seed);
      }
      if (this.m_security_flags.handshake == (byte) 1)
      {
        this.m_handshake_blowfish_key = Security.NextUInt64();
        this.m_value_x = Security.NextUInt32() & (uint) int.MaxValue;
        this.m_value_g = Security.NextUInt32() & (uint) int.MaxValue;
        this.m_value_p = Security.NextUInt32() & (uint) int.MaxValue;
        this.m_value_A = this.G_pow_X_mod_P(this.m_value_p, this.m_value_x, this.m_value_g);
        packet.WriteUInt64(this.m_handshake_blowfish_key);
        packet.WriteUInt32(this.m_value_g);
        packet.WriteUInt32(this.m_value_p);
        packet.WriteUInt32(this.m_value_A);
      }
      this.m_outgoing_packets.Add(packet);
    }

    private void Handshake(ushort packet_opcode, PacketReader packet_data, bool packet_encrypted)
    {
      if (packet_encrypted)
        throw new Exception("[SecurityAPI::Handshake] Received an illogical (encrypted) handshake packet.");
      if (this.m_client_security)
      {
        if (this.m_security_flags.handshake == (byte) 0)
        {
          if (packet_opcode == (ushort) 36864)
          {
            this.m_accepted_handshake = !this.m_accepted_handshake ? true : throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (duplicate 0x9000).");
          }
          else
          {
            if (packet_opcode == (ushort) 20480)
              throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (0x5000 with no handshake).");
            throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (programmer error).");
          }
        }
        else if (packet_opcode == (ushort) 36864)
        {
          if (!this.m_started_handshake)
            throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (out of order 0x9000).");
          this.m_accepted_handshake = !this.m_accepted_handshake ? true : throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (duplicate 0x9000).");
        }
        else
        {
          if (packet_opcode != (ushort) 20480)
            throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (programmer error).");
          this.m_started_handshake = !this.m_started_handshake ? true : throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (duplicate 0x5000).");
          this.m_value_B = packet_data.ReadUInt32();
          this.m_client_key = packet_data.ReadUInt64();
          this.m_value_K = this.G_pow_X_mod_P(this.m_value_p, this.m_value_x, this.m_value_B);
          ulong val = Security.MAKELONGLONG_(this.m_value_A, this.m_value_B);
          this.KeyTransformValue(ref val, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_K)) & 3U));
          this.m_blowfish.Initialize(BitConverter.GetBytes(val));
          this.m_client_key = BitConverter.ToUInt64(this.m_blowfish.Decode(BitConverter.GetBytes(this.m_client_key)), 0);
          val = Security.MAKELONGLONG_(this.m_value_B, this.m_value_A);
          this.KeyTransformValue(ref val, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_B)) & 7U));
          val = (long) this.m_client_key == (long) val ? Security.MAKELONGLONG_(this.m_value_A, this.m_value_B) : throw new Exception("[SecurityAPI::Handshake] Client signature error.");
          this.KeyTransformValue(ref val, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_K)) & 3U));
          this.m_blowfish.Initialize(BitConverter.GetBytes(val));
          this.m_challenge_key = Security.MAKELONGLONG_(this.m_value_A, this.m_value_B);
          this.KeyTransformValue(ref this.m_challenge_key, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_A)) & 7U));
          this.m_challenge_key = BitConverter.ToUInt64(this.m_blowfish.Encode(BitConverter.GetBytes(this.m_challenge_key)), 0);
          this.KeyTransformValue(ref this.m_handshake_blowfish_key, this.m_value_K, (byte) 3);
          this.m_blowfish.Initialize(BitConverter.GetBytes(this.m_handshake_blowfish_key));
          byte num = Security.FromSecurityFlags(new Security.SecurityFlags()
          {
            handshake_response = (byte) 1
          });
          Packet packet = new Packet((ushort) 20480);
          packet.WriteUInt8(num);
          packet.WriteUInt64(this.m_challenge_key);
          this.m_outgoing_packets.Add(packet);
        }
      }
      else
      {
        if (packet_opcode != (ushort) 20480)
          throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (programmer error).");
        byte num = packet_data.ReadByte();
        Security.SecurityFlags securityFlags = Security.ToSecurityFlags(num);
        if (this.m_security_flag == (byte) 0)
        {
          this.m_security_flag = num;
          this.m_security_flags = securityFlags;
        }
        if (securityFlags.blowfish == (byte) 1)
        {
          this.m_initial_blowfish_key = packet_data.ReadUInt64();
          this.m_blowfish.Initialize(BitConverter.GetBytes(this.m_initial_blowfish_key));
        }
        if (securityFlags.security_bytes == (byte) 1)
        {
          this.m_seed_count = packet_data.ReadUInt32();
          this.m_crc_seed = packet_data.ReadUInt32();
          this.SetupCountByte(this.m_seed_count);
        }
        if (securityFlags.handshake == (byte) 1)
        {
          this.m_handshake_blowfish_key = packet_data.ReadUInt64();
          this.m_value_g = packet_data.ReadUInt32();
          this.m_value_p = packet_data.ReadUInt32();
          this.m_value_A = packet_data.ReadUInt32();
          this.m_value_x = Security.NextUInt32() & (uint) int.MaxValue;
          this.m_value_B = this.G_pow_X_mod_P(this.m_value_p, this.m_value_x, this.m_value_g);
          this.m_value_K = this.G_pow_X_mod_P(this.m_value_p, this.m_value_x, this.m_value_A);
          ulong val = Security.MAKELONGLONG_(this.m_value_A, this.m_value_B);
          this.KeyTransformValue(ref val, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_K)) & 3U));
          this.m_blowfish.Initialize(BitConverter.GetBytes(val));
          this.m_client_key = Security.MAKELONGLONG_(this.m_value_B, this.m_value_A);
          this.KeyTransformValue(ref this.m_client_key, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_B)) & 7U));
          this.m_client_key = BitConverter.ToUInt64(this.m_blowfish.Encode(BitConverter.GetBytes(this.m_client_key)), 0);
        }
        if (securityFlags.handshake_response == (byte) 1)
        {
          this.m_challenge_key = packet_data.ReadUInt64();
          ulong val = Security.MAKELONGLONG_(this.m_value_A, this.m_value_B);
          this.KeyTransformValue(ref val, this.m_value_K, (byte) ((uint) Security.LOBYTE_(Security.LOWORD_(this.m_value_A)) & 7U));
          if ((long) this.m_challenge_key != (long) BitConverter.ToUInt64(this.m_blowfish.Encode(BitConverter.GetBytes(val)), 0))
            throw new Exception("[SecurityAPI::Handshake] Server signature error.");
          this.KeyTransformValue(ref this.m_handshake_blowfish_key, this.m_value_K, (byte) 3);
          this.m_blowfish.Initialize(BitConverter.GetBytes(this.m_handshake_blowfish_key));
        }
        if (securityFlags.handshake == (byte) 1 && securityFlags.handshake_response == (byte) 0)
        {
          if (this.m_started_handshake || this.m_accepted_handshake)
            throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (duplicate 0x5000).");
          Packet packet = new Packet((ushort) 20480);
          packet.WriteUInt32(this.m_value_B);
          packet.WriteUInt64(this.m_client_key);
          this.m_outgoing_packets.Insert(0, packet);
          this.m_started_handshake = true;
        }
        else
        {
          if (this.m_accepted_handshake)
            throw new Exception("[SecurityAPI::Handshake] Received an illogical handshake packet (duplicate 0x5000).");
          Packet packet1 = new Packet((ushort) 36864);
          Packet packet2 = new Packet((ushort) 8193, true, false);
          packet2.WriteAscii(this.m_identity_name);
          packet2.WriteUInt8(this.m_identity_flag);
          this.m_outgoing_packets.Insert(0, packet2);
          this.m_outgoing_packets.Insert(0, packet1);
          this.m_started_handshake = true;
          this.m_accepted_handshake = true;
        }
      }
    }

    private byte[] FormatPacket(ushort opcode, byte[] data, bool encrypted)
    {
      ushort num1 = data.Length < 32768 ? (ushort) data.Length : throw new Exception("[SecurityAPI::FormatPacket] Payload is too large!");
      PacketWriter packetWriter = new PacketWriter();
      packetWriter.Write(num1);
      packetWriter.Write(opcode);
      packetWriter.Write((ushort) 0);
      packetWriter.Write(data);
      packetWriter.Flush();
      if (encrypted && (this.m_security_flags.blowfish == (byte) 1 || this.m_security_flags.security_bytes == (byte) 1 && this.m_security_flags.blowfish == (byte) 0))
      {
        long offset = packetWriter.BaseStream.Seek(0L, SeekOrigin.Current);
        ushort num2 = (ushort) ((uint) num1 | 32768U);
        packetWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
        packetWriter.Write(num2);
        packetWriter.Flush();
        packetWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
      }
      if (!this.m_client_security && this.m_security_flags.security_bytes == (byte) 1)
      {
        long offset = packetWriter.BaseStream.Seek(0L, SeekOrigin.Current);
        byte countByte = this.GenerateCountByte(true);
        packetWriter.BaseStream.Seek(4L, SeekOrigin.Begin);
        packetWriter.Write(countByte);
        packetWriter.Flush();
        byte checkByte = this.GenerateCheckByte(packetWriter.GetBytes());
        packetWriter.BaseStream.Seek(5L, SeekOrigin.Begin);
        packetWriter.Write(checkByte);
        packetWriter.Flush();
        packetWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
      }
      if (encrypted && this.m_security_flags.blowfish == (byte) 1)
      {
        byte[] bytes = packetWriter.GetBytes();
        byte[] buffer = this.m_blowfish.Encode(bytes, 2, bytes.Length - 2);
        packetWriter.BaseStream.Seek(2L, SeekOrigin.Begin);
        packetWriter.Write(buffer);
        packetWriter.Flush();
      }
      else if (encrypted && (this.m_security_flags.security_bytes == (byte) 1 && this.m_security_flags.blowfish == (byte) 0))
      {
        long offset = packetWriter.BaseStream.Seek(0L, SeekOrigin.Current);
        packetWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
        packetWriter.Write(num1);
        packetWriter.Flush();
        packetWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
      }
      return packetWriter.GetBytes();
    }

    private bool HasPacketToSend()
    {
      if (this.m_outgoing_packets.Count == 0)
        return false;
      if (this.m_accepted_handshake)
        return true;
      Packet outgoingPacket = this.m_outgoing_packets[0];
      return outgoingPacket.Opcode == (ushort) 20480 || outgoingPacket.Opcode == (ushort) 36864;
    }

    private KeyValuePair<TransferBuffer, Packet> GetPacketToSend()
    {
      Packet packet = this.m_outgoing_packets.Count != 0 ? this.m_outgoing_packets[0] : throw new Exception("[SecurityAPI::GetPacketToSend] No packets are avaliable to send.");
      this.m_outgoing_packets.RemoveAt(0);
      if (packet.Massive)
      {
        ushort num = 0;
        PacketWriter packetWriter1 = new PacketWriter();
        PacketWriter packetWriter2 = new PacketWriter();
        byte[] bytes1 = packet.GetBytes();
        PacketReader packetReader = new PacketReader(bytes1);
        TransferBuffer transferBuffer = new TransferBuffer(4089, 0, bytes1.Length);
        while (transferBuffer.Size > 0)
        {
          PacketWriter packetWriter3 = new PacketWriter();
          int count = transferBuffer.Size > 4089 ? 4089 : transferBuffer.Size;
          packetWriter3.Write((byte) 0);
          packetWriter3.Write(bytes1, transferBuffer.Offset, count);
          transferBuffer.Offset += count;
          transferBuffer.Size -= count;
          packetWriter2.Write(this.FormatPacket((ushort) 24589, packetWriter3.GetBytes(), false));
          ++num;
        }
        PacketWriter packetWriter4 = new PacketWriter();
        packetWriter4.Write((byte) 1);
        packetWriter4.Write((short) num);
        packetWriter4.Write(packet.Opcode);
        packetWriter1.Write(this.FormatPacket((ushort) 24589, packetWriter4.GetBytes(), false));
        packetWriter1.Write(packetWriter2.GetBytes());
        byte[] bytes2 = packetWriter1.GetBytes();
        packet.Lock();
        return new KeyValuePair<TransferBuffer, Packet>(new TransferBuffer(bytes2, 0, bytes2.Length, true), packet);
      }
      bool encrypted = packet.Encrypted;
      if (!this.m_client_security && this.m_enc_opcodes.Contains(packet.Opcode))
        encrypted = true;
      byte[] buffer = this.FormatPacket(packet.Opcode, packet.GetBytes(), encrypted);
      packet.Lock();
      return new KeyValuePair<TransferBuffer, Packet>(new TransferBuffer(buffer, 0, buffer.Length, true), packet);
    }

    public Security()
    {
      this.m_value_x = 0U;
      this.m_value_g = 0U;
      this.m_value_p = 0U;
      this.m_value_A = 0U;
      this.m_value_B = 0U;
      this.m_value_K = 0U;
      this.m_seed_count = 0U;
      this.m_crc_seed = 0U;
      this.m_initial_blowfish_key = 0UL;
      this.m_handshake_blowfish_key = 0UL;
      this.m_count_byte_seeds = new byte[3];
      this.m_count_byte_seeds[0] = (byte) 0;
      this.m_count_byte_seeds[1] = (byte) 0;
      this.m_count_byte_seeds[2] = (byte) 0;
      this.m_client_key = 0UL;
      this.m_challenge_key = 0UL;
      this.m_client_security = false;
      this.m_security_flag = (byte) 0;
      this.m_security_flags = new Security.SecurityFlags();
      this.m_accepted_handshake = false;
      this.m_started_handshake = false;
      this.m_identity_flag = (byte) 0;
      this.m_identity_name = "SR_Client";
      this.m_outgoing_packets = new List<Packet>();
      this.m_incoming_packets = new List<Packet>();
      this.m_enc_opcodes = new List<ushort>();
      this.m_enc_opcodes.Add((ushort) 8193);
      this.m_enc_opcodes.Add((ushort) 24832);
      this.m_enc_opcodes.Add((ushort) 24833);
      this.m_enc_opcodes.Add((ushort) 24834);
      this.m_enc_opcodes.Add((ushort) 24835);
      this.m_enc_opcodes.Add((ushort) 24839);
      this.m_blowfish = new Blowfish();
      this.m_recv_buffer = new TransferBuffer(8192);
      this.m_current_buffer = (TransferBuffer) null;
      this.m_massive_count = (ushort) 0;
      this.m_massive_packet = (Packet) null;
      this.m_class_lock = new object();
    }

    public void ChangeIdentity(string name, byte flag)
    {
      lock (this.m_class_lock)
      {
        this.m_identity_name = name;
        this.m_identity_flag = flag;
      }
    }

    public void GenerateSecurity(bool blowfish, bool security_bytes, bool handshake)
    {
      lock (this.m_class_lock)
      {
        Security.SecurityFlags flags = new Security.SecurityFlags();
        if (blowfish)
        {
          flags.none = (byte) 0;
          flags.blowfish = (byte) 1;
        }
        if (security_bytes)
        {
          flags.none = (byte) 0;
          flags.security_bytes = (byte) 1;
        }
        if (handshake)
        {
          flags.none = (byte) 0;
          flags.handshake = (byte) 1;
        }
        if (!blowfish && !security_bytes && !handshake)
          flags.none = (byte) 1;
        this.GenerateSecurity(flags);
      }
    }

    public void AddEncryptedOpcode(ushort opcode)
    {
      lock (this.m_class_lock)
      {
        if (this.m_enc_opcodes.Contains(opcode))
          return;
        this.m_enc_opcodes.Add(opcode);
      }
    }

    public void Send(Packet packet)
    {
      if (packet.Opcode == (ushort) 20480 || packet.Opcode == (ushort) 36864)
        return;
      lock (this.m_class_lock)
        this.m_outgoing_packets.Add(packet);
    }

    public void Recv(byte[] buffer, int offset, int length) => this.Recv(new TransferBuffer(buffer, offset, length, true));

    public void Recv(TransferBuffer raw_buffer)
    {
      List<TransferBuffer> transferBufferList = new List<TransferBuffer>();
      lock (this.m_class_lock)
      {
        int num1 = raw_buffer.Size - raw_buffer.Offset;
        int num2 = 0;
        while (num1 > 0)
        {
          int count = num1;
          int num3 = this.m_recv_buffer.Buffer.Length - this.m_recv_buffer.Size;
          if (count > num3)
            count = num3;
          num1 -= count;
          Buffer.BlockCopy((Array) raw_buffer.Buffer, raw_buffer.Offset + num2, (Array) this.m_recv_buffer.Buffer, this.m_recv_buffer.Size, count);
          this.m_recv_buffer.Size += count;
          num2 += count;
          while (this.m_recv_buffer.Size > 0)
          {
            if (this.m_current_buffer == null)
            {
              if (this.m_recv_buffer.Size >= 2)
              {
                int num4 = (int) this.m_recv_buffer.Buffer[1] << 8 | (int) this.m_recv_buffer.Buffer[0];
                int num5;
                if ((num4 & 32768) > 0)
                {
                  int num6 = num4 & (int) short.MaxValue;
                  num5 = this.m_security_flags.blowfish != (byte) 1 ? num6 + 6 : 2 + this.m_blowfish.GetOutputLength(num6 + 4);
                }
                else
                  num5 = num4 + 6;
                this.m_current_buffer = new TransferBuffer(num5, 0, num5);
              }
              else
                break;
            }
            int num7 = this.m_current_buffer.Size - this.m_current_buffer.Offset;
            if (num7 > this.m_recv_buffer.Size)
              num7 = this.m_recv_buffer.Size;
            Buffer.BlockCopy((Array) this.m_recv_buffer.Buffer, 0, (Array) this.m_current_buffer.Buffer, this.m_current_buffer.Offset, num7);
            this.m_current_buffer.Offset += num7;
            this.m_recv_buffer.Size -= num7;
            if (this.m_recv_buffer.Size > 0)
              Buffer.BlockCopy((Array) this.m_recv_buffer.Buffer, num7, (Array) this.m_recv_buffer.Buffer, 0, this.m_recv_buffer.Size);
            if (this.m_current_buffer.Size == this.m_current_buffer.Offset)
            {
              this.m_current_buffer.Offset = 0;
              transferBufferList.Add(this.m_current_buffer);
              this.m_current_buffer = (TransferBuffer) null;
            }
            else
              break;
          }
        }
        if (transferBufferList.Count <= 0)
          return;
        foreach (TransferBuffer transferBuffer in transferBufferList)
        {
          bool flag = false;
          int num3 = (int) transferBuffer.Buffer[1] << 8 | (int) transferBuffer.Buffer[0];
          if ((num3 & 32768) > 0)
          {
            if (this.m_security_flags.blowfish == (byte) 1)
            {
              num3 &= (int) short.MaxValue;
              flag = true;
            }
            else
              num3 &= (int) short.MaxValue;
          }
          if (flag)
          {
            byte[] numArray1 = this.m_blowfish.Decode(transferBuffer.Buffer, 2, transferBuffer.Size - 2);
            byte[] numArray2 = new byte[6 + num3];
            Buffer.BlockCopy((Array) BitConverter.GetBytes((ushort) num3), 0, (Array) numArray2, 0, 2);
            Buffer.BlockCopy((Array) numArray1, 0, (Array) numArray2, 2, 4 + num3);
            transferBuffer.Buffer = (byte[]) null;
            transferBuffer.Buffer = numArray2;
          }
          PacketReader packet_data = new PacketReader(transferBuffer.Buffer);
          int length = (int) packet_data.ReadUInt16();
          ushort num4 = packet_data.ReadUInt16();
          byte num5 = packet_data.ReadByte();
          byte num6 = packet_data.ReadByte();
          if (this.m_client_security && this.m_security_flags.security_bytes == (byte) 1)
          {
            byte countByte = this.GenerateCountByte(true);
            if ((int) num5 == (int) countByte)
              ;
            if ((flag || this.m_security_flags.security_bytes == (byte) 1 && this.m_security_flags.blowfish == (byte) 0) && (flag || this.m_enc_opcodes.Contains(num4)))
            {
              length |= 32768;
              Buffer.BlockCopy((Array) BitConverter.GetBytes((ushort) length), 0, (Array) transferBuffer.Buffer, 0, 2);
            }
            transferBuffer.Buffer[5] = (byte) 0;
            byte checkByte = this.GenerateCheckByte(transferBuffer.Buffer);
            if ((int) num6 == (int) checkByte)
              ;
            transferBuffer.Buffer[4] = (byte) 0;
            if ((flag || this.m_security_flags.security_bytes == (byte) 1 && this.m_security_flags.blowfish == (byte) 0) && (flag || this.m_enc_opcodes.Contains(num4)))
            {
              length &= (int) short.MaxValue;
              Buffer.BlockCopy((Array) BitConverter.GetBytes((ushort) length), 0, (Array) transferBuffer.Buffer, 0, 2);
            }
          }
          if (num4 == (ushort) 20480 || num4 == (ushort) 36864)
          {
            this.Handshake(num4, packet_data, flag);
            Packet packet = new Packet(num4, flag, false, transferBuffer.Buffer, 6, length);
            packet.Lock();
            this.m_incoming_packets.Add(packet);
          }
          else
          {
            if (this.m_client_security && !this.m_accepted_handshake)
              throw new Exception("[SecurityAPI::Recv] The client has not accepted the handshake.");
            if (num4 == (ushort) 24589)
            {
              if (packet_data.ReadByte() == (byte) 1)
              {
                this.m_massive_count = packet_data.ReadUInt16();
                this.m_massive_packet = new Packet(packet_data.ReadUInt16(), flag, true);
              }
              else
              {
                if (this.m_massive_packet == null)
                  throw new Exception("[SecurityAPI::Recv] A malformed 0x600D packet was received.");
                this.m_massive_packet.WriteUInt8Array(packet_data.ReadBytes(length - 1));
                --this.m_massive_count;
                if (this.m_massive_count == (ushort) 0)
                {
                  this.m_massive_packet.Lock();
                  this.m_incoming_packets.Add(this.m_massive_packet);
                  this.m_massive_packet = (Packet) null;
                }
              }
            }
            else
            {
              Packet packet = new Packet(num4, flag, false, transferBuffer.Buffer, 6, length);
              packet.Lock();
              this.m_incoming_packets.Add(packet);
            }
          }
        }
      }
    }

    public List<KeyValuePair<TransferBuffer, Packet>> TransferOutgoing()
    {
      List<KeyValuePair<TransferBuffer, Packet>> keyValuePairList = (List<KeyValuePair<TransferBuffer, Packet>>) null;
      lock (this.m_class_lock)
      {
        if (this.HasPacketToSend())
        {
          keyValuePairList = new List<KeyValuePair<TransferBuffer, Packet>>();
          while (this.HasPacketToSend())
            keyValuePairList.Add(this.GetPacketToSend());
        }
      }
      return keyValuePairList;
    }

    public List<Packet> TransferIncoming()
    {
      List<Packet> packetList = (List<Packet>) null;
      lock (this.m_class_lock)
      {
        if (this.m_incoming_packets.Count > 0)
        {
          packetList = this.m_incoming_packets;
          this.m_incoming_packets = new List<Packet>();
        }
      }
      return packetList;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    private class SecurityFlags
    {
      [FieldOffset(0)]
      public byte none;
      [FieldOffset(1)]
      public byte blowfish;
      [FieldOffset(2)]
      public byte security_bytes;
      [FieldOffset(3)]
      public byte handshake;
      [FieldOffset(4)]
      public byte handshake_response;
      [FieldOffset(5)]
      public byte _6;
      [FieldOffset(6)]
      public byte _7;
      [FieldOffset(7)]
      public byte _8;
    }
  }
}
