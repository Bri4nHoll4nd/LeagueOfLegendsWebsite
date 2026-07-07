namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Model.V1
{
    public class V1Champion
    {
        public Guid Id { get; set; }
        public string Version { get; set; }
        public string RiotId { get; set; }
        public string RiotKey { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public V1Info Info { get; set; }
        public V1Image Image { get; set; }
        public string Tag1 { get; set; }
        public string? Tag2 { get; set; }
        public string Partype { get; set; }
        public V1Stats Stats { get; set; }
    }
}
