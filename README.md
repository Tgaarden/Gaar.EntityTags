﻿# Gaar.EntityTags
Easy handling of Etags with MVC attribute

# How to
1. Add EntityTagAttribute to functions in controller to add Etag to the response
2. Call EntityTagHelper.UpdateEtagValueInCache() to refresh the Etag value
3. Call EntityTagHelper.GetEtagValueInCache() to get current Etag value
4. Both functions has default parameter to spesify cache key

C#
[EntityTag]
public string GetSomeData()
{
	... logic ...
}

Web.Config - if not auto included
<system.webServer>
   <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Expose-Headers" value="ETag, Retry-After"/>
      </customHeaders>
   </httpProtocol>
</system.webServer>
