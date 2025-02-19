# Translation set sources

Translation sets are populated from a source. A source could be anything, like a
database, a resource file or a hardcoded class, as long as it implements the
interface `ITranslationSetSource`. The core TechTolk package does not contain
any implementations, but extension packages exist.

These source packages are part of the TechTolk repository:

* [TechTolk.Sources.Resx](resx.md) - Reads translations from embedded resource files
* [TechTolk.Sources.Json](json.md) - Reads translations from JSON files
* ... more to come...

You can always roll your own source. See [Create your own source](diy.md) page
for instructions implementing your own translation set source.