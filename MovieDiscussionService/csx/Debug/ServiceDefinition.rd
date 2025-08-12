<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" name="MovieDiscussionService" generation="1" functional="0" release="0" Id="a87a5db5-5253-4bb1-a8b1-dcb9a7d25eb7" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
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
    <implementation Id="20d3e144-ac60-4b7d-955c-826658fe34a6" ref="Microsoft.RedDog.Contract\ServiceContract\MovieDiscussionServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="06da4112-4d87-42b5-96e6-84c3da44c878" ref="Microsoft.RedDog.Contract\Interface\MovieDiscussionService_WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>