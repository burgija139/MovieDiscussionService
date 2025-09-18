<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" name="MovieDiscussionService" generation="1" functional="0" release="0" Id="242940a7-9a1c-4f7f-82a7-e5ec698d594b" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MovieDiscussionServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HealthMonitoringService:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:HealthMonitoringService:HttpIn" />
          </inToChannel>
        </inPort>
        <inPort name="MovieDiscussionService_WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/LB:MovieDiscussionService_WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HealthMonitoringService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthMonitoringService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthMonitoringServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapHealthMonitoringServiceInstances" />
          </maps>
        </aCS>
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
        <aCS name="NotificationService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapNotificationService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapNotificationServiceInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MapWorkerRoleServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HealthMonitoringService:HttpIn">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService/HttpIn" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:MovieDiscussionService_WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHealthMonitoringService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapHealthMonitoringServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceInstances" />
          </setting>
        </map>
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
        <map name="MapNotificationService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceInstances" />
          </setting>
        </map>
        <map name="MapWorkerRoleServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HealthMonitoringService" generation="1" functional="0" release="0" software="E:\Fakultet\Cloud\Projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\HealthMonitoringService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="50002" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthMonitoringService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="MovieDiscussionService_WebRole" generation="1" functional="0" release="0" software="E:\Fakultet\Cloud\Projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\MovieDiscussionService_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MovieDiscussionService_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
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
        <groupHascomponents>
          <role name="NotificationService" generation="1" functional="0" release="0" software="E:\Fakultet\Cloud\Projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\NotificationService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;NotificationService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/NotificationServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRoleService" generation="1" functional="0" release="0" software="E:\Fakultet\Cloud\Projekat\MovieDiscussionService\MovieDiscussionService\csx\Debug\roles\WorkerRoleService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRoleService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;MovieDiscussionService_WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot; /&gt;&lt;r name=&quot;WorkerRoleService&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/WorkerRoleServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MovieDiscussionService_WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="NotificationServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRoleServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthMonitoringServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HealthMonitoringServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="MovieDiscussionService_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="NotificationServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRoleServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HealthMonitoringServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="MovieDiscussionService_WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="NotificationServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRoleServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="64783f87-2352-45d1-9f0b-3ed13c5a68d6" ref="Microsoft.RedDog.Contract\ServiceContract\MovieDiscussionServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="f6a8f2d1-acd7-47bc-9d57-248909450d47" ref="Microsoft.RedDog.Contract\Interface\HealthMonitoringService:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/HealthMonitoringService:HttpIn" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="ff92d8d0-a8da-42e2-bdd4-1db2e08a43cb" ref="Microsoft.RedDog.Contract\Interface\MovieDiscussionService_WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MovieDiscussionService/MovieDiscussionServiceGroup/MovieDiscussionService_WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>