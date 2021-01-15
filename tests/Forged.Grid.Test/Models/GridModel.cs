using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forged.Grid.Tests
{
    public class GridModel
    {
        [Display(Name = "Text")]
        public string? Text { get; set; }

        [Display(Name = "Text", ShortName = "Txt")]
        public string? ShortText { get; set; }

        public IHtmlContent? Content { get; set; }
        public bool? NIsChecked { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? NDate { get; set; }
        public TestEnum? NEnum { get; set; }
        public DateTime Date { get; set; }
        public TestEnum Enum { get; set; }
        public string? Name { get; set; }
        public int? NSum { get; set; }
        public Guid? NGuid { get; set; }
        public int Sum { get; set; }
        public Guid Guid { get; set; }

        public TestEnum EnumField { get; set; }
        public Guid GuidField { get; set; }
        public sbyte SByteField { get; set; }
        public byte ByteField { get; set; }
        public short Int16Field { get; set; }
        public ushort UInt16Field { get; set; }
        public int Int32Field { get; set; }
        public uint UInt32Field { get; set; }
        public long Int64Field { get; set; }
        public ulong UInt64Field { get; set; }
        public float SingleField { get; set; }
        public double DoubleField { get; set; }
        public decimal DecimalField { get; set; }
        public bool BooleanField { get; set; }
        public DateTime DateTimeField { get; set; }

        public TestEnum? NullableEnumField { get; set; }
        public Guid? NullableGuidField { get; set; }
        public sbyte? NullableSByteField { get; set; }
        public byte? NullableByteField { get; set; }
        public short? NullableInt16Field { get; set; }
        public ushort? NullableUInt16Field { get; set; }
        public int? NullableInt32Field { get; set; }
        public uint? NullableUInt32Field { get; set; }
        public long? NullableInt64Field { get; set; }
        public ulong? NullableUInt64Field { get; set; }
        public float? NullableSingleField { get; set; }
        public double? NullableDoubleField { get; set; }
        public decimal? NullableDecimalField { get; set; }
        public bool? NullableBooleanField { get; set; }
        public DateTime? NullableDateTimeField { get; set; }

        public string?[]? NullableArrayField { get; set; }
        public List<string?>? NullableListField { get; set; }
        public IEnumerable<string?>? NullableEnumerableField { get; set; }

        public string? StringField { get; set; }

        public GridModel? Child { get; set; }
    }

    public enum TestEnum
    {
        [Display(Name = "1st")]
        First,

        [Display(Name = "2nd")]
        Second
    }
}
