using System;
using System.Collections.Generic;
using System.Text;

namespace FissureTracker
{
    public class Ability
    {
        public string name { get; set; }
        public int total { get; set; }
        public int type { get; set; }
        public int? totalReduced { get; set; }
    }

    public class Source
    {
        public string name { get; set; }
        public int total { get; set; }
        public string type { get; set; }
        public int? totalReduced { get; set; }
    }

    public class Damage
    {
        public int total { get; set; }
        public int activeTime { get; set; }
        public int activeTimeReduced { get; set; }
        public List<Ability> abilities { get; set; }
        public List<object> damageAbilities { get; set; }
        public List<Source> sources { get; set; }
        public int? totalReduced { get; set; }
        public int? overheal { get; set; }
    }

    public class Healing
    {
        public int total { get; set; }
        public int activeTime { get; set; }
        public int activeTimeReduced { get; set; }
        public List<object> abilities { get; set; }
        public List<object> damageAbilities { get; set; }
        public List<object> sources { get; set; }
    }

    public class Ability2
    {
        public string name { get; set; }
        public int guid { get; set; }
        public int type { get; set; }
        public string abilityIcon { get; set; }
    }

    public class Event
    {
        public int timestamp { get; set; }
        public string type { get; set; }
        public int sourceID { get; set; }
        public int sourceInstance { get; set; }
        public bool sourceIsFriendly { get; set; }
        public int targetID { get; set; }
        public bool targetIsFriendly { get; set; }
        public Ability2 ability { get; set; }
        public int fight { get; set; }
        public int hitType { get; set; }
        public int amount { get; set; }
        public int unmitigatedAmount { get; set; }
        public int overkill { get; set; }
        public int? mitigated { get; set; }
        public int? absorbed { get; set; }
    }

    public class KillingBlow
    {
        public string name { get; set; }
        public int guid { get; set; }
        public int type { get; set; }
        public string abilityIcon { get; set; }
    }

    public class Entry
    {
        public string name { get; set; }
        public int id { get; set; }
        public int guid { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public int timestamp { get; set; }
        public Damage damage { get; set; }
        public Healing healing { get; set; }
        public int fight { get; set; }
        public int deathWindow { get; set; }
        public int overkill { get; set; }
        public List<Event> events { get; set; }
        public KillingBlow killingBlow { get; set; }
    }

    public class Data2
    {
        public List<Entry> entries { get; set; }
    }

    public class Table
    {
        public Data2 data { get; set; }
    }

    public class Datum
    {
        public string title { get; set; }
        public string code { get; set; }
        public Table table { get; set; }
    }

    public class Reports
    {
        public List<Datum> data { get; set; }
    }

    public class ReportData
    {
        public Reports reports { get; set; }
    }

    public class Data
    {
        public ReportData reportData { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }
}
