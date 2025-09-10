<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" name="MovieDiscussionService" generation="1" functional="0" release="0" Id="d9333078-914f-49f4-9bb3-00e4657c7ed8" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MovieDiscussionServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MovieDiscussionService_WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:MovieDiscussionService_WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MovieDiscussionService_WebRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapMovieDiscussionService_WebRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="MovieDiscussionService_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapMovieDiscussionService_WebRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MovieDiscussionService_WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMovieDiscussionService_WebRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapMovieDiscussionService_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MovieDiscussionService_WebRole" generation="1" functional="0" release="0" software="C:\Users\BURGIJA\Desktop\CLOUD\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\MovieDiscussionService_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MovieDiscussionService_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MovieDiscussionService_WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MovieDiscussionService_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MovieDiscussionService_WebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="9c7391a2-ed34-46ca-8b73-7b45bb2f4c46" ref="Microsoft.RedDog.Contract\ServiceContract\MovieDiscussionServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="79d66c1c-d4de-4d59-a29d-ec7ad09d7e88" ref="Microsoft.RedDog.Contract\Interface\MovieDiscussionService_WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>