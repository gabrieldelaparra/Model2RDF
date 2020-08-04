# A simple XML/JSON to NTriples converter

The tool can be tested at: [https://gabrieldelaparra.github.io/Model2RDF/]

- Uses `keys` that end with `id` or XML attributes (`@id`) as identifiers for each object.
- Blank nodes are assigned a `GUID` as identifier.
- The `http://model2.rdf` URI is used as namespace for all terms.
- Does not take namespaces from XML files (yet) nor transforms yet.
- The webapplication is hosted on github using Blazor and Webassembly.

Please leave your feedback/issues.