# Python script cannot be a "one-liner" due to try-catch block

import sys, json;

try:
  print(json.load(sys.stdin)['document_guid']);
except json.JSONDecodeError as err:
  # Instead of a JSON string, likely received an error message, so display to user
  print(err.doc, file=sys.stderr)
  sys.exit(1)
