# .NETCoreCacheSurvy
This is a simple cache survy POC 

You can get the performance of all cache mechanism by api and monitor the log.

You need a text file to generate init Data.

Origin Key File: a long string without any '\n', '\t', and space. 

Using api 'MakeKeyValue', the string in Origin Key File will be separated every 5 chars as a key, pairing with a GUID as value. 

These keys are stored in Key File separated with '\n'. These values are stored in value File separated with '\n'.
