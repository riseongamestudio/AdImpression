namespace RiseOn.AdImpression {
    public readonly struct AdImpression {
        public readonly string Source;
        public readonly string UnitId;
        public readonly string Format;
        public readonly Mediation Mediation;
        public readonly double Value;
        public readonly string Currency;

        public AdImpression(
            string source
          , string unitId
          , string format
          , Mediation mediation
          , double value
          , string currency) {
            Source = source;
            UnitId = unitId;
            Format = format;
            Mediation = mediation;
            Value = value;
            Currency = currency;
        }
    }
}