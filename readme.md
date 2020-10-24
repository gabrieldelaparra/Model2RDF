# A simple XML/JSON to NTriples converter

The tool can be tested at: [https://gabrieldelaparra.github.io/ModelToRdf/]

- Uses `keys` that end with `id` or XML attributes (`@id`) as identifiers for each object.
- Blank nodes are assigned a `GUID` as identifier (I need to read something like [this](http://www.aidanhogan.com/docs/blank_nodes_jws.pdf) to see why they are required)
- The `http://model2.rdf` URI is used as namespace for all terms.
- Does not take namespaces from XML files (yet) nor transforms yet.
- The webapplication is hosted on github using Blazor and Webassembly.
- Only a few unittests have been implemented: bugs are surely there.

Please leave your feedback/issues.
