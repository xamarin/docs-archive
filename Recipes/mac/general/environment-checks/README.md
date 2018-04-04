---
id: F4D6F2C3-0B5C-1977-00BA-E38AAB660FC3
title: "Environment Checks"
---

This recipe shows how to make various environment checks from your code to handle different runtime environments.

## Check the macOS Version

The macOS operating system version can be checked like this:

```
if (NSProcessInfo.ProcessInfo.IsOperatingSystemAtLeastVersion (new NSOperatingSystemVersion (10, 10, 0))) {
                // API available on macOS 10.12+
} else {
   // Code to support earlier iOS versions
}
```

**Note** This is only available in 10.10 and above.

