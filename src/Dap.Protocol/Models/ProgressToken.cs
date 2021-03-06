﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public struct ProgressToken : IEquatable<ProgressToken>, IEquatable<long>, IEquatable<string>
    {
        private long? _long;
        private string? _string;

        public ProgressToken(long value)
        {
            _long = value;
            _string = null;
        }

        public ProgressToken(string value)
        {
            _long = null;
            _string = value;
        }

        public bool IsLong => _long.HasValue;

        public long Long
        {
            get => _long ?? 0;
            set {
                _string = null;
                _long = value;
            }
        }

        public bool IsString => _string != null;

        public string String
        {
            get => _string ?? string.Empty;
            set {
                _string = value;
                _long = null;
            }
        }

        public static implicit operator ProgressToken(long value) => new ProgressToken(value);

        public static implicit operator ProgressToken(string value) => new ProgressToken(value);

        public override bool Equals(object obj) =>
            obj is ProgressToken token &&
            Equals(token);

        public override int GetHashCode()
        {
            var hashCode = 1456509845;
            hashCode = hashCode * -1521134295 + IsLong.GetHashCode();
            hashCode = hashCode * -1521134295 + Long.GetHashCode();
            hashCode = hashCode * -1521134295 + IsString.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(String);
            return hashCode;
        }

        public bool Equals(ProgressToken other) =>
            IsLong == other.IsLong &&
            Long == other.Long &&
            IsString == other.IsString &&
            String == other.String;

        public bool Equals(long other) => IsLong && Long == other;
        public bool Equals(string other) => IsString && String == other;

        private string DebuggerDisplay => IsString ? String : IsLong ? Long.ToString() : "";

        /// <inheritdoc />
        public override string ToString() => DebuggerDisplay;
    }
}
