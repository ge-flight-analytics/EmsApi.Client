
/**
 * An automatically generated TypeScript definition file with types used in the EMS REST API.
 * Because this file is generated don't make any changes here. Instead re-run the generator.
 */

declare module ems.api.v1 {
    /**
     * Encapsulates metadata information for a parameter or analytic in EMS.
     */
    export interface IMetadata {
        /**
         * The metadata for the object.
         */
        values: ems.api.v1.IMetadataItem[];
    }
    /**
     * Encapsulates a single piece of metadata.
     */
    export interface IMetadataItem {
        /**
         * The metadata key.
         */
        key: string;
        /**
         * The metadata value.
         */
        value: any;
    }
    /**
     * Encapsulates a value and the location it occurred at, in seconds from stat of file.
     */
    export interface IOffsetValue {
        /**
         * The offset of the value.
         */
        offset: number;
        /**
         * The value related to the offset at <see cref="Offset"/>.
         */
        value: any;
    }
    /**
     * Encapsulates the data returned when requesting values through the API. 
     * Values.Length will always equal Offsets.Length.
     */
    export interface IOffsetValuePairs {
        /**
         * The offsets of the samples, in seconds from start of file.
         */
        offsets: number[];
        /**
         * The values related to the values at <see cref="Offsets"/>.
         */
        values: any[];
    }
    /**
     * Represents a message used to provide information to users about an action that took place in an API request.
     */
    export interface IApiMessage {
        /**
         * A categorization of the message.
         */
        type: ems.api.v1.admin.ApiMessageType | string;
        /**
         * A string message describing an event that occurred in an API action or event.
         */
        message: string;
    }
}

declare module ems.api.v1.admin {
    /**
     * Represents the supported OAuth grant types that may be associated with an API client.
     */
    const enum ApiClientGrantType {
        /**
         * The "trusted" grant type where an Active Directory attribute name and value pair can be exchanged for
         * a token
         */
        trusted = 0,
        /**
         * The "password" grant type where an Active Directory username and password can be exchanged for a token
         */
        password = 1,
    }
    /**
     * Represents the different message types that make up an API message.
     */
    const enum ApiMessageType {
        /**
         * An API action or event was neither a success or failure, but includes a message with information that 
         * could be useful to consumers.
         */
        warning = 0,
        /**
         * An API action or event encountered an error, which was the cause of a 4xx status code response.
         */
        error = 1,
        /**
         * An API action or event was successful.
         */
        success = 2,
    }
    /**
     * Represents an API client application that consumes REST services hosted in the website.
     */
    export interface IApiClient {
        /**
         * The unique integer identifier of the API client
         */
        id: number;
        /**
         * The unique, user friendly, client ID of the client, used when clients request tokens to access the API
         */
        clientId: string;
        /**
         * A brief summary describing the API client and its purpose
         */
        description: string;
        /**
         * The grant type that may be used by the client to access the API
         */
        grantType: ems.api.v1.admin.ApiClientGrantType | string;
        /**
         * Indicates whether the API client is active and can currently consume the API
         */
        enabled: boolean;
        /**
         * The date and time at which the API client record was created
         */
        created: string;
        /**
         * The date and time at which the API client record was last updated
         */
        updated: string;
    }
    /**
     * Represents a new API client application that consumes REST services hosted in the website that can be provided
     * when creating a new client record.
     */
    export interface IApiClientNew {
        /**
         * The unique, user friendly, client ID of the client used when clients request tokens to access the API
         */
        clientId: string;
        /**
         * The client secret value used when clients request tokens to access the API
         */
        clientSecret: string;
        /**
         * The grant type that may be used by the client to access the API
         */
        grantType?: ems.api.v1.admin.ApiClientGrantType;
        /**
         * An optional brief summary describing the API client and its purpose
         */
        description: string;
    }
    /**
     * Represents an object that can be provided to change an API client's secret value.
     */
    export interface IApiClientSecretReset {
        /**
         * The client secret value used when clients request tokens to access the API
         */
        newClientSecret: string;
    }
    /**
     * Encapsulates the basic properties of a dashboard used by EMS web application users to view EMS-related data.
     */
    export interface IDashboard {
        /**
         * The unique integer identifier of the dashboards.
         */
        id: number;
        /**
         * The title of the dashboard typically rendered in the page header of a displayed dashboard.
         */
        title: string;
        /**
         * A detailed description that describes the contents of the dashboard.
         */
        description: string;
    }
    /**
     * Represents a structure for specifying one or existing dashboard IDs.
     */
    export interface IDashboardIds {
        /**
         * An array specifying one or more unique IDs of existing dashboards.
         */
        ids: number[];
    }
    /**
     * Represents a message used to provide information to users about an action that took place in an API request.
     */
    export interface IDashboardImport {
        /**
         * The deserialized <see cref="Dashboard"/> instance created in the import process.
         */
        dashboard: ems.api.v1.admin.IDashboard;
        /**
         * A list of messages indicating the status of the import and any extra information that may be useful to 
         * convey to a user performing an import.
         */
        messages: any;
        /**
         * A value indicating whether any of the messages included in this instance is an error.
         */
        hasErrors: boolean;
        /**
         * A value indicating whether any of the messages included in this instance is a warning.
         */
        hasWarnings: boolean;
    }
    /**
     * Represents a message used to provide information to users about an action that took place in an API request.
     */
    export interface IDashboardsImport {
        /**
         * The list of imported dashboards and information gathered for each performed import.
         */
        imports: any;
        /**
         * A summary messaged intended to be shown to users of what happened in the import process.
         */
        summary: ems.api.v1.IApiMessage;
    }
    /**
     * Represents an EMS system that can be used and accessed by users of the website.
     */
    export interface IEmsSystem {
        /**
         * The unique integer identifier of the EMS system.
         */
        id: number;
        /**
         * The unique, user friendly, name of the EMS system.
         */
        name: string;
        /**
         * The "Dir Adi" URI where the EMS system installation is located, used to access the EMS system's resources.
         */
        dirAdi: string;
        /**
         * The path to the collection location indicating where data collections for this EMS system should go.
         */
        dirCollection: string;
        /**
         * A brief summary describing the EMS system and its purpose.
         */
        description: string;
    }
    /**
     * Represents a group that is used to control security for website users that are members of the group.
     */
    export interface IGroup {
        /**
         * The unique integer identifier of the group.
         */
        id: number;
        /**
         * The unique, user friendly, name of the group.
         */
        name: string;
        /**
         * A brief summary describing the group and its members and purpose.
         */
        description: string;
    }
    /**
     * Represents a user account that can be used to access the website.
     */
    export interface IUser {
        /**
         * The unique integer identifier of the user.
         */
        id: number;
        /**
         * The Active Directory username of the user, typically in the form of "domain\user.name". If not in an 
         * environment with multiple domains, the domain name is not needed.
         */
        username: string;
        /**
         * Indicates whether the account lockout policy applies to this user, causing them to be locked out of the
         * application for a configured amount of time after failed login attempts.
         */
        lockoutPolicyEnabled: boolean;
        /**
         * The list of roles to which the user is a member. Only values "Admin", "Developer", "DashboardEditor", and
         * "DashboardViewer" are supported.
         */
        roles: any;
        /**
         * The Tableau username to which this user is linked. This user name is only needed if different than the user 
         * name.
         */
        tableauUsername: string;
        /**
         * A read-only value indicating the last time the user logged into the application.
         */
        lastLogin?: string;
        /**
         * A read-only value indicating the time that the user will no longer be locked out. Null if the user is not 
         * currently locked out.
         */
        lockoutEndDateUtc?: string;
        /**
         * A read-only value indicating the number of times the user failed to login.
         */
        accessFailedCount: number;
    }
}

declare module ems.api.v1.analytic {
    /**
     * Represents a group in the exposed tree of analytics for an EMS installation.
     */
    export interface IAnalyticGroup {
        /**
         * The identifier of the group.
         */
        id: string;
        /**
         * The name of the group.
         */
        name: string;
        /**
         * The description of the group.
         */
        description: string;
    }
    /**
     * Represents the contents of an analytic group.
     */
    export interface IAnalyticGroupContents {
        /**
         * An in-order listing of the groups contained in the specified group.
         */
        groups: ems.api.v1.analytic.IAnalyticGroup[];
        /**
         * An in-order listing of the analytics contained in the specified group.
         */
        analytics: ems.api.v1.analytic.IAnalyticInfo[];
    }
    /**
     * Encapsulates information about an analytic.
     */
    export interface IAnalyticInfo {
        /**
         * The identifier for the analytic.
         */
        id: string;
        /**
         * The name of the analytic.
         */
        name: string;
        /**
         * The description of the analytic.
         */
        description: string;
        /**
         * The units of the analytic.
         */
        units: string;
    }
    /**
     * Identifies an offset request that specifies the identifier and offsets in a RESTful EMS API request.
     */
    export interface IMultipleByOffsetContents {
        /**
         * The analytic to retrieve.
         */
        id: string;
        /**
         * The offsets for which we want to retrieve values.
         */
        offsets: number[];
    }
}

declare module ems.api.v1.asset {
    /**
     * An aircraft in the system refers to an airframe with a particular tail number.
     */
    export interface IAircraft {
        /**
         * The identifier of the aircraft within the system.
         */
        id: number;
        /**
         * The description for this aircraft.
         */
        description: string;
        /**
         * Each aircraft can be mapped to 0-N fleets in the system. 
         * Because a fleet represents a configuration for a airframe and those configurations can change over time,
         * it stands to reason that each aircraft can be mapped to more than one of them.
         */
        fleetIds: number[];
    }
    /**
     * Encapsulates the definition of an airport.
     */
    export interface IAirport {
        /**
         * The identifier of the airport within the system.
         */
        id: number;
        /**
         * The ICAO code associated with this airport.
         */
        codeIcao: string;
        /**
         * The FAA / Regional code associated with this airport.
         */
        codeFaa: string;
        /**
         * The preferred code to use for display. This is typically either the ICAO or FAA code, but can be overridden by the user.
         */
        codePreferred: string;
        /**
         * The name of the airport.
         */
        name: string;
        /**
         * The city of the airport.
         */
        city: string;
        /**
         * The country of the airport.
         */
        country: string;
        /**
         * The latitude of the airport.
         */
        latitude: number;
        /**
         * The longitude of the airport.
         */
        longitude: number;
        /**
         * The elevation of the airport, in feet.
         */
        elevation: number;
    }
    /**
     * Represents a 'Fleet' in the EMS system.
     * A fleet in this sense is a group of aircraft using the same configuration.
     * There may be several fleets representing a particular model of aircraft, each showing a specific configuration
     * of the airframe.
     */
    export interface IFleet {
        /**
         * The identifier of the fleet within the system.
         */
        id: number;
        /**
         * The description for this fleet.
         */
        description: string;
    }
    /**
     * A flight phase is a coarse division of a flight into sections, such as takeoff, climb and enroute.
     * The flight phases are used as inputs into some analytics.
     */
    export interface IFlightPhase {
        /**
         * The identifier of the flight phase within the system.
         */
        id: number;
        /**
         * The description for this phase of flight.
         */
        description: string;
    }
}

declare module ems.api.v1.dataSource {
    /**
     * Indicates how the date and time information is encoded in a date time field.
     */
    const enum DateTimeType {
        /**
         * A complete date and time UTC value.
         */
        dateTimeUtc = 0,
        /**
         * UTC date information only (as "year/month/date").
         */
        dateOnlyUtc = 1,
        /**
         * UTC year and month information only (as "year/month").
         */
        yearMonthUtc = 2,
    }
    /**
     * Indicates the data type of a field.
     */
    const enum FieldType {
        /**
         * A data type containing only boolean values.
         */
        boolean = 0,
        /**
         * A data type containing date and/or time.
         */
        dateTime = 1,
        /**
         * A data type representing a map of discrete values.
         */
        discrete = 2,
        /**
         * A data type representing a floating point or integer number.
         */
        number = 3,
        /**
         * A data type representing a string of characters.
         */
        string = 4,
    }
    /**
     * Represents the different types of arguments in a filter.
     */
    const enum FilterArgumentType {
        /**
         * Indicates that the filter argument type wasn't specified or is unknown.
         */
        none = 0,
        /**
         * An argument representing a filter tree with an operation and one or more arguments.
         */
        filter = 1,
        /**
         * An argument representing a reference to a data source field.
         */
        field = 2,
        /**
         * An argument representing a constant value.
         */
        constant = 3,
    }
    /**
     * Indicate the operator used in a data source query to filter the data returned.
     */
    const enum FilterOperator {
        /**
         * Represents the "is true" truth check operator.
         */
        isTrue = 0,
        /**
         * Represents the "is false" truth check operator.
         */
        isFalse = 1,
        /**
         * Represents the "is null" unary operator.
         */
        isNull = 2,
        /**
         * Represents the "not null" unary operator.
         */
        isNotNull = 3,
        /**
         * Represents the "and" boolean operator.
         */
        and = 4,
        /**
         * Represents the "or" boolean operator.
         */
        or = 5,
        /**
         * Represents the "not" boolean operator.
         */
        not = 6,
        /**
         * Represents the "in" collection matching operator.
         */
        in = 7,
        /**
         * Represents the "not in" collection matching operator.
         */
        notIn = 8,
        /**
         * Represents the addition arithmetic operator.
         */
        plus = 9,
        /**
         * Represents the subtraction arithmetic operator.
         */
        minus = 10,
        /**
         * Represents the multiplication arithmetic operator.
         */
        multiply = 11,
        /**
         * Represents the division arithmetic operator.
         */
        divide = 12,
        /**
         * Represents the modulo arithmetic operator.
         */
        modulo = 13,
        /**
         * Represents the equality comparison operator.
         */
        equal = 14,
        /**
         * Represents the inequality comparison operator.
         */
        notEqual = 15,
        /**
         * Represents the "less than" comparison operator.
         */
        lessThan = 16,
        /**
         * Represents the "less than or equal" comparison operator.
         */
        lessThanOrEqual = 17,
        /**
         * Represents the "greater than" comparison operator.
         */
        greaterThan = 18,
        /**
         * Represents the "greater than or equal" comparison operator.
         */
        greaterThanOrEqual = 19,
        /**
         * Represents the "between inclusive" between operator.
         */
        betweenInclusive = 20,
        /**
         * Represents the "between lower exclusive" between operator.
         */
        betweenLowerExclusive = 21,
        /**
         * Represents the "between upper exclusive" between operator.
         */
        betweenUpperExclusive = 22,
        /**
         * Represents the "between exclusive" between operator.
         */
        betweenExclusive = 23,
        /**
         * Represents the "between inclusive" not between operator.
         */
        notBetweenInclusive = 24,
        /**
         * Represents the "between lower exclusive" not between operator.
         */
        notBetweenLowerExclusive = 25,
        /**
         * Represents the "between upper exclusive" not between operator.
         */
        notBetweenUpperExclusive = 26,
        /**
         * Represents the "between exclusive" not between operator.
         */
        notBetweenExclusive = 27,
        /**
         * Represents the "include" integer range operator.
         */
        integerRangeInclude = 28,
        /**
         * Represents the "exclude" integer range operator.
         */
        integerRangeExclude = 29,
        /**
         * Represents the "include" real range operator.
         */
        realRangeInclude = 30,
        /**
         * Represents the "exclude" real range operator.
         */
        realRangeExclude = 31,
        /**
         * Represents the "like" string operator.
         */
        like = 32,
        /**
         * Represents the "not like" string operator.
         */
        notLike = 33,
        /**
         * Represents the "all words" string operator.
         */
        allWords = 34,
        /**
         * Represents the "any words" string operator.
         */
        anyWords = 35,
        /**
         * Represents the "no words" string operator.
         */
        noWords = 36,
        /**
         * Represents the "relative" date operator.
         */
        dateRelative = 37,
        /**
         * Represents the "month range" date operator.
         */
        dateSpecificMonths = 38,
        /**
         * Represents the "specific month" date operator.
         */
        dateSpecificMonth = 39,
        /**
         * Represents the "before month" date operator.
         */
        dateBeforeMonth = 40,
        /**
         * Represents the "on or after month" date operator.
         */
        dateOnAfterMonth = 41,
        /**
         * Represents the "day range" date operator.
         */
        dateSpecificDays = 42,
        /**
         * Represents the "specific day" date operator.
         */
        dateSpecificDay = 43,
        /**
         * Represents the "before day" date operator.
         */
        dateBeforeDay = 44,
        /**
         * Represents the "on or after day" date operator.
         */
        dateOnAfterDay = 45,
        /**
         * Represents the "exact time range" date-time operator.
         */
        dateTimeRange = 46,
        /**
         * Represents the "before exact time" date-time operator.
         */
        dateTimeBefore = 47,
        /**
         * Represents the "on or after exact time" date-time operator.
         */
        dateTimeOnAfter = 48,
        /**
         * Represents the "particular quarter of any year" date operator.
         */
        dateQuarterOfYear = 49,
        /**
         * Represents the "particular month of any year" date operator.
         */
        dateMonthOfYear = 50,
        /**
         * Represents the "particular day of the week" date operator.
         */
        dateDayOfWeek = 51,
        /**
         * Represents the "particular hour of the day" date-time operator.
         */
        dateTimeHourOfDay = 52,
    }
    /**
     * Indicates the suggested method for editing numeric field values.
     */
    const enum NumberEditStyle {
        /**
         * A manual input text box is recommended.
         */
        manualInput = 0,
        /**
         * A numeric slider is recommended.
         */
        slider = 1,
    }
    /**
     * Indicate the way in which numeric field values should be interpreted in query results.
     */
    const enum NumberInterpretation {
        /**
         * The number value represents a piece of data, such as number of years or velocity. Data values can usually 
         * be ordered, added, subtracted, etc.
         */
        dataValue = 0,
        /**
         * The number identifies something, such as a record number. Identifier values generally should not be 
         * manipulated arithmetically.
         */
        identifier = 1,
    }
    /**
     * Indicates the type of aggregation to use when returning a column of data in a data source query result.
     */
    const enum QueryAggregation {
        /**
         * Represents no aggregation.
         */
        none = 0,
        /**
         * Represents an average aggregation type.
         */
        avg = 1,
        /**
         * Represents a count aggregation type.
         */
        count = 2,
        /**
         * Represents a maximum value aggregation type.
         */
        max = 3,
        /**
         * Represents a minimum value aggregation type.
         */
        min = 4,
        /**
         * Represents a statistical standard deviation aggregation type.
         */
        stdev = 5,
        /**
         * Represents a sum aggregation type.
         */
        sum = 6,
        /**
         * Represents a statistical variance aggregation type.
         */
        var = 7,
    }
    /**
     * Indicate the ordering behavior to use in the results returned from a data source query.
     */
    const enum QueryOrderBy {
        /**
         * Result values are returned in ascending order. Note that sorting is always done based on actual result
         * values, not formatted display values that may be configured to be returned.
         */
        asc = 0,
        /**
         * Results values are returned in descending order. Note that sorting is always done based on actual result
         * values, not formatted display values that may be configured to be returned.
         */
        desc = 1,
    }
    /**
     * Indicates the supported types of value formatting that can be applied to query results when they are returned.
     */
    const enum QueryValueFormat {
        /**
         * No value formatting is used. Values returned can be used for further processing or calculations.
         */
        none = 0,
        /**
         * Format values in a format appropriate for displaying values to the user.
         */
        display = 1,
    }
    /**
     * The desired behavior of a relative date filter operator.
     */
    const enum RelativeDateMode {
        /**
         * The current unit (e.g. today, this month, this year).
         */
        current = 0,
        /**
         * The previous N units (e.g. yesterday, last 6 months, last 2 years).
         */
        previous = 1,
        /**
         * The current unit plus N previous units (e.g. today plus the last 6 days).
         */
        currentPlusPrevious = 2,
        /**
         * Before the previous N units (e.g. before yesterday, before the last 6 months).
         */
        beforePrevious = 3,
    }
    /**
     * The date unit to use with a relative date filter operator.
     */
    const enum RelativeDateUnit {
        /**
         * The filter duration value is in minutes.
         */
        minute = 0,
        /**
         * The filter duration value is in days.
         */
        day = 1,
        /**
         * The filter duration value is in weeks.
         */
        week = 2,
        /**
         * The filter duration value is in months.
         */
        month = 3,
        /**
         * The filter duration value is in quarters.
         */
        quarter = 4,
        /**
         * The filter duration value is in years.
         */
        year = 5,
    }
    /**
     * Indicates the suggested method for displaying and/or editing values for a string field.
     */
    const enum StringDisplayStyle {
        /**
         * Indicates the values should be displayed and/or edited with a single-line text box.
         */
        singleLine = 0,
        /**
         * Indicates the values should be displayed and/or edited with a multi-line text box.
         */
        multiLine = 1,
    }
    /**
     * The time zone handling to use with a relative date filter.
     */
    const enum TimeZoneHandling {
        /**
         * Convert database DateTime values to local time before performing relative filtering.
         */
        local = 0,
        /**
         * Keep database DateTime values as UTC when performing relative filtering.
         */
        utc = 1,
    }
    /**
     * Represents the tabular results of a data source query obtained from an async query.
     */
    export interface IAsyncQueryData {
        /**
         * An array of JSON string arrays, where each entry is a row in the results set.
         */
        rows: string[];
    }
    /**
     * Passed back to the client when an async query is started. This contains query header
     * information, and a unique ID, with which the user can later read data for this query.
     */
    export interface IAsyncQueryInfo {
        /**
         * The ID of the query; this will be used when making subsequent calls to the query APIs.
         */
        id: string;
        /**
         * The number of seconds of inactivity after which an async query is eligible for automatic deletion.
         */
        inactivityTimeout: number;
        /**
         * An ordered list of header column information, describing the fields included in the results and matching 
         * the order of columns in the resulting data rows.
         */
        header: ems.api.v1.dataSource.IQueryResultHeader[];
    }
    /**
     * Represents a data source type exposed by and EMS installation.
     */
    export interface IDataSource {
        /**
         * The unique string identifier for the data source
         */
        id: string;
        /**
         * A plural display name for the data source
         */
        pluralName: string;
        /**
         * A singular display name for the data source
         */
        singularName: string;
    }
    /**
     * Represents a folder in the data source tree of an EMS installation.
     */
    export interface IDataSourceGroup {
        /**
         * The unique string identifier for the data source group
         */
        id: string;
        /**
         * The display name for the data source group
         */
        name: string;
        /**
         * An ordered listing of child data source groups contained in this group
         */
        groups: ems.api.v1.dataSource.IDataSourceGroup[];
        /**
         * An ordered listing of child data sources contained in this group
         */
        dataSources: ems.api.v1.dataSource.IDataSource[];
    }
    /**
     * Represents a field storing date time information in a data source.
     */
    export interface IDateTimeField extends ems.api.v1.dataSource.IField {
        /**
         * The type of date time information encoded in the field
         */
        dateTimeType: ems.api.v1.dataSource.DateTimeType | string;
        /**
         * If set to true, the date time values should be displayed in local time format rather than UTC
         */
        dateTimeLocal: boolean;
    }
    /**
     * Represents a field storing discrete information in a data source.
     */
    export interface IDiscreteField extends ems.api.v1.dataSource.IField {
        /**
         * A map of the raw values the discrete field can assume to their user-friendly string representations
         */
        discreteValues: any;
    }
    /**
     * Represents an individual field that can be accessed within a data source.
     */
    export interface IField {
        /**
         * The unique string identifier for the field
         */
        id: string;
        /**
         * The data type of the underlying data stored for the field
         */
        type: ems.api.v1.dataSource.FieldType | string;
        /**
         * The display name for the field
         */
        name: string;
    }
    /**
     * Represents a folder in the data source tree of an EMS installation.
     */
    export interface IFieldGroup {
        /**
         * The unique string identifier for the field group
         */
        id: string;
        /**
         * The display name for the field group
         */
        name: string;
        /**
         * An ordered list of child groups contained in a field group
         */
        groups: ems.api.v1.dataSource.IFieldGroup[];
        /**
         * An ordered list of child fields contained in a field group
         */
        fields: ems.api.v1.dataSource.IField[];
    }
    /**
     * Represents the operations and arguments that can be used to filter a data source query.
     */
    export interface IFilter {
        /**
         * The unique string identifier of the operation to perform
         */
        operator: ems.api.v1.dataSource.FilterOperator | string;
        /**
         * An ordered list of arguments to provide the operator. The requirements for the arguments depend on which 
         * operation is being performed
         */
        args: ems.api.v1.dataSource.IFilterArgument[];
    }
    /**
     * Represents an argument in a filter.
     */
    export interface IFilterArgument {
        /**
         * The type of the filter argument, describing the role of the argument.
         */
        type: ems.api.v1.dataSource.FilterArgumentType | string;
        /**
         * The value represented by the filter argument.
         */
        value: any;
    }
    /**
     * Represents a column of data that can be used in a group-by clause of a data source query.
     */
    export interface IGroupByColumn {
        /**
         * The unique string identifier of the field to use in a group-by clause of a query
         */
        fieldId: string;
    }
    /**
     * Represents a field storing numeric information in a data source.
     */
    export interface INumericField extends ems.api.v1.dataSource.IField {
        /**
         * A value indicating the way in which the numeric field values should be interpreted
         */
        numberInterpretation: ems.api.v1.dataSource.NumberInterpretation | string;
        /**
         * The units for the values in the field, e.g. "seconds"
         */
        numberUnits: string;
        /**
         * The minimum value that can be returned in the results for the field
         */
        numberMinValue: number;
        /**
         * The minimum value that can be returned in the results for the field
         */
        numberMaxValue: number;
        /**
         * The suggested method for editing the numeric field values
         */
        numberEditStyle: ems.api.v1.dataSource.NumberEditStyle | string;
    }
    /**
     * Represents a column of data that can be used in a order-by clause of a data source query.
     */
    export interface IOrderByColumn {
        /**
         * The unique string identifier of the field to use in a order-by clause of a query
         */
        fieldId: string;
        /**
         * An optional aggregate operation to perform on the column. Omission of this property results in no aggregate 
         * operation being used in the ordering
         */
        aggregate?: ems.api.v1.dataSource.QueryAggregation;
        /**
         * The ordering behavior to use for the column values. The omission of this property results in ascending order
         */
        order?: ems.api.v1.dataSource.QueryOrderBy;
    }
    /**
     * Represents the options used to make up a data source query.
     */
    export interface IQuery {
        /**
         * An array specifying the fields to select and return as columns in the query results
         */
        select: ems.api.v1.dataSource.ISelectColumn[];
        /**
         * An array specifying the fields by which to group aggregate operations. If not specified, no grouping
         * is performed
         */
        groupBy: ems.api.v1.dataSource.IGroupByColumn[];
        /**
         * An array specifying the selected columns by which to order result rows. Results are always ordered by the
         * results value, not by the display formatted value (configured to be returned by the format property). If
         * not specified, results are returned in default database ordering
         */
        orderBy: ems.api.v1.dataSource.IOrderByColumn[];
        /**
         * A filter used to narrow the query results
         */
        filter: ems.api.v1.dataSource.IFilter;
        /**
         * Value formatting that should be performed on results values before returning. If not specified, no results
         * formatting is performed
         */
        format: ems.api.v1.dataSource.QueryValueFormat | string;
        /**
         * Whether to remove duplicate rows from query results <b>before formatting</b> the results. Using a format
         * value other than 'none' can reduce results precision, leading to cases where two distinct rows appear
         * equivalent after formatting is applied. Defaults to false if not specified
         */
        distinct?: boolean;
        /**
         * Identifies a subset of rows to return.
         * The subset is formed by selecting only the first N rows of the set, where N is a positive integer provided 
         * as the value of this property. If not specified, all rows are returned
         */
        top?: number;
    }
    /**
     * Represents the tabular results of a data source query.
     */
    export interface IQueryResult {
        /**
         * An ordered list of header column information, describing the fields included in the results and matching 
         * the order of columns in the resulting data rows
         */
        header: ems.api.v1.dataSource.IQueryResultHeader[];
        /**
         * An array of arrays representing the resulting data rows of a query. Each inner array represents a single 
         * results row
         */
        rows: any[][];
        /**
         * Indicates whether the rows returned are a partial query result because the maximum amount of rows that can 
         * be returned has been reached.
         */
        isPartialResult: boolean;
    }
    /**
     * Represents a header column in data source query result.
     */
    export interface IQueryResultHeader {
        /**
         * The unique string identifier of the field associated with this column
         */
        id: string;
        /**
         * The user-friendly display name of the field associated with this column
         */
        name: string;
        /**
         * The units of values of the field associated with this column. This value may be empty if not applicable
         */
        units: string;
    }
    /**
     * Represents a column of data that can be selected in a data source query.
     */
    export interface ISelectColumn {
        /**
         * The unique string identifier of the field to select in a query.
         */
        fieldId: string;
        /**
         * An optional aggregate operation to perform on the column. Omission of this property results in no aggregate 
         * operation being performed.
         */
        aggregate?: ems.api.v1.dataSource.QueryAggregation;
    }
    /**
     * Represents a field storing string information in a data source.
     */
    export interface IStringField extends ems.api.v1.dataSource.IField {
        /**
         * The maximum number of characters in the string. A value of -1 means "largest possible"
         */
        stringLength: number;
        /**
         * The suggested method for displaying and/or editing string field values
         */
        stringDisplayStyle: ems.api.v1.dataSource.StringDisplayStyle | string;
    }
}

declare module ems.api.v1.parameter {
    /**
     * Represents a "folder" in the FDW logical parameters tree for an EMS installation.
     * Example: "Flight Dynamics\Altitudes\AFE\"
     */
    export interface IParameterGroup {
        /**
         * The unique identifier for this parameter group.
         */
        id: string;
        /**
         * The path of the parameter group.
         * example: [Flight Dynamics][Altitudes][AFE]
         */
        path: string[];
        /**
         * The description of the parameter group.
         * example: AFE
         */
        description: string;
    }
    /**
     * Represents the contents of a logical parameter group.
     */
    export interface IParameterGroupContents {
        /**
         * An in-order listing of the groups contained in the specified group.
         */
        groups: ems.api.v1.parameter.IParameterGroup[];
        /**
         * An in-order listing of the parameters contained in the specified group.
         */
        parameters: ems.api.v1.parameter.IParameterInfo[];
    }
    /**
     * Encapsulates information about a logical parameter in EMS.
     */
    export interface IParameterInfo {
        /**
         * The unique identifier of the parameter.
         */
        id: number;
        /**
         * The description of the parameter.
         */
        description: string;
        /**
         * The units of the parameter.
         */
        units: string;
    }
}

declare module ems.api.v1.parameterSet {
    /**
     * Encapsulates the some data defining a ParameterSet.
     */
    export interface IParameterSet {
        /**
         * The name of the ParameterSet.
         */
        name: string;
        /**
         * An optional description of the ParameterSet.
         */
        description: string;
        /**
         * An array of the parameters contained in the ParameterSet.
         */
        items: ems.api.v1.parameterSet.IParameterSetItem[];
    }
    /**
     * A container for parameter sets.
     */
    export interface IParameterSetGroup {
        /**
         * The name of the group.
         */
        name: string;
        /**
         * The id of the group. This should be a relative path.
         */
        groupId: string;
        /**
         * An array of groups contained by this group.
         */
        groups: ems.api.v1.parameterSet.IParameterSetGroup[];
        /**
         * An array of parameter sets contained by this group.
         */
        sets: ems.api.v1.parameterSet.IParameterSet[];
    }
    /**
     * A container for parameter sets.
     */
    export interface IParameterSetItem {
        /**
         * The index of the chart that this analytic info belongs to, or null if no index specified.
         */
        chartIndex?: number;
        /**
         * The analytic that is represented by this parameter set item.
         */
        parameter: ems.api.v1.analytic.IAnalyticInfo;
    }
}

declare module ems.api.v1.searchFlights {
    /**
     * Represents the criteria used when searching the database of flights.
     */
    export interface ISearchFlightsCriteria {
        /**
         * A comma-separated list of flight record identifiers.
         * A null string means no filter will be applied.
         */
        flightRecords: string;
        /**
         * The first date for which we should return records. 
         * A null string means no filter will be applied.
         */
        startDateValue: string;
        /**
         * The last date for which we should return records.
         * A null string means no filter will be applied.
         */
        endDateValue: string;
        /**
         * The location that the flight originated from.
         * A null string means no filter will be applied.
         */
        takeoffAirport: string;
        /**
         * The destination of the flight.
         */
        landingAirport: string;
        /**
         * The flight number associated with the flight leg.
         * A null string means no filter will be applied.
         */
        flightNumber: string;
        /**
         * The aircraft (tail number) for the flight.
         * A null string means no filter will be applied.
         */
        aircraft?: number;
        /**
         * The fleet type for this flight.
         */
        fleet?: number;
        /**
         * The maximum number of flights to return in the results set. Defaults to no limit.
         */
        maxFlights?: number;
        /**
         * If true, we will only return results where the flight data exists.
         */
        flightDataExistsFilter?: boolean;
        /**
         * The format requested by the client for the results. This value will be transferred to the 
         * ResultsModel so that the consumer of that model has this value.
         * Ideally, this value would be accessed via the host of the ResultsModel, but because our
         * FlightSearcher renders the HTML server-side this is difficult to do without passing into 
         * the ResultsModel explicitly.
         */
        requestedFormatting: any;
    }
}

declare module ems.api.v1.tableau {
    /**
     * Represents information about the associated Tableau server.
     */
    export interface ITableauServer {
        /**
         * The root URL of the Tableau server with site.
         */
        url: string;
        /**
         * The site being used.
         */
        site: string;
    }
    /**
     * Represents information that can be used to access Tableau content related to an associated Tableau server.
     */
    export interface ITrusted {
        /**
         * The URL that can be used to start a trusted session with the Tableau server.
         */
        trustedUrl: string;
        /**
         * The root URL of the Tableau server with site.
         */
        url: string;
        /**
         * The site that the TrustedUrl is able to access when used.
         */
        site: string;
    }
}

declare module ems.api.v1.trajectory {
    /**
     * Encapsulates a data point defining the userOptions config for a Google Earth export.
     */
    export interface ITrajectoryConfiguration {
        /**
         * A unique identifier for this type of KML trajectory. 
         * Typically this just the name of the file sans extension.
         */
        trajectoryId: string;
        /**
         * A description of what kind of trajectory this KML type generates.
         */
        description: string;
    }
    /**
     * Encapsulates a data point defining the positional information [lat/long/alt] 
     * for a sample and the location it occurred at, in seconds from start of file.
     */
    export interface ITrajectoryValue {
        /**
         * The offset of the value, in seconds from start of file.
         */
        offset: number;
        /**
         * The latitude related to the offset at <see cref="Offset"/>.
         */
        latitude: number;
        /**
         * The longitude related to the offset at <see cref="Offset"/>.
         */
        longitude: number;
        /**
         * The altitude related to the offset at <see cref="Offset"/>.
         */
        altitude: number;
    }
    /**
     * Encapsulates the data returned when requesting trajectory values through the API.
     */
    export interface ITrajectoryValueArray {
        /**
         * The points in the generated trajectory.
         */
        values: ems.api.v1.trajectory.ITrajectoryValue[];
    }
}

declare module ems.api.v1.transfer {
    /**
     * Possible states of an activity in regards to download or flight processing.
     */
    const enum UploadProcessingActivityState {
        /**
         * The activity has not processed the download or flight.
         */
        notProcessed = 0,
        /**
         * The activity has processed the download or flight successfully.
         */
        processed = 1,
        /**
         * The activity has processed the download or flight and failed in doing so.
         */
        failure = 2,
    }
    /**
     * The type of the data to upload.
     */
    const enum UploadType {
        /**
         * The transferred data represents an EMS collection. This should be a zip file encrypted with a
         * client-supplied password. It will contain the raw files that would go in data/ in an EMS collection
         * dir; the transfer's metadata will be used to generate an ident.xml.
         */
        emsCollectionZipped = 0,
        /**
         * The transferred data is just a test and may be discarded as soon as it's transferred.
         */
        testTransfer = 1,
        /**
         * The transferred data represents an EMS collection. This file will be assumed to be the entire
         * contents of the data/ directory in an EMS collection dir; the transfer's metadata will be used
         * to generate an ident.xml. The metadata should include a field called "Filename" with the file's
         * original filename (or whatever it should be named once it lands).
         */
        emsCollectionRaw = 2,
    }
    /**
     * A list of possible values indicating an upload's current completion status.
     */
    const enum UploadCompleteness {
        /**
         * There was no data at all.
         */
        empty = 0,
        /**
         * We could not retrieve any data at all, even though it's there.
         */
        completeFailure = 1,
        /**
         * All data was retrieved successfully.
         */
        completeSuccess = 2,
        /**
         * We could retrieve some data, and what we could retrieve was good.
         */
        partialGood = 3,
        /**
         * We could retrieve some data, but what we could retrieve was bad.
         */
        partialBad = 4,
        /**
         * The amount of data transferred is unknown.
         */
        unknown = 5,
    }
    /**
     * A list of the possible metadata items that may be used when creating a transfer.
     * Items that are not on this list may also be used and will follow the collection into EMS.
     */
    const enum UploadMetadataType {
        /**
         * The original name of the file, when transferring a non-zipped file.
         */
        filename = 0,
        /**
         * The volume label of the original media, if applicable.
         */
        mediaVolumeLabel = 1,
        /**
         * The ID of the original media, if applicable.
         */
        mediaID = 2,
        /**
         * Value taken from the UploadCompleteness enum.
         */
        dataCompleteness = 3,
        /**
         * Aircraft ID, if applicable.
         */
        aircraftID = 4,
        /**
         * Comments, if desired.
         */
        comments = 5,
        /**
         * The username of the download user, if applicable.
         */
        downloadUserName = 6,
        /**
         * The location of the download, if applicable.
         */
        downloadLocation = 7,
        /**
         * The machine name of the download, if applicable.
         */
        downloadMachineName = 8,
        /**
         * The number of bytes available on the source media, if applicable; note that if this and
         * OnMedia aren't filled out, the number of bytes in the upload will be used.
         */
        bytesOnMedia = 9,
        /**
         * The number of bytes copied, if applicable; note that if this and OnMedia aren't filled
         * out, the number of bytes in the upload will be used.
         */
        bytesCopied = 10,
        /**
         * Number of skipped files, if applicable.
         */
        skippedFileCount = 11,
    }
    /**
     * The state of the upload.
     */
    const enum UploadState {
        /**
         * The data are still being transferred; the total amount is not equal to the total size yet.
         */
        transferring = 0,
        /**
         * The data have been fully transferred, but post-processing (CRC check, extract, etc) has not been started.
         */
        waitingProcessing = 1,
        /**
         * The data have been fully transferred, but post-processing (CRC check, extract, etc) has not completed yet.
         */
        processing = 2,
        /**
         * The data have been fully ingested successfully. Note that this does not include any EMS status.
         */
        processedSuccess = 3,
        /**
         * The data have been ingested, but not successfully. The transfer will need to be retried.
         */
        processedFailure = 4,
        /**
         * The transfer was left too long with no movement in the Transferring state, and is assumed
         * to be abandoned.
         */
        abandonedTransfer = 5,
        /**
         * The processing was attempted for too long, and is assumed to be permanently broken.
         */
        abandonedProcessing = 6,
        /**
         * The transfer was canceled.
         */
        canceled = 7,
    }
    /**
     * Passed back to the client when a multi-part upload is started. This contains further
     * instructions on how to proceed with the upload, including the transfer ID to pass to
     * future calls, as well as any change of URL for the actual chunk uploads.
     */
    export interface IUploadParameters {
        /**
         * The ID of the transfer; this will be used when making subsequent calls to the upload APIs.
         */
        transferId: string;
        /**
         * The URL for the transfer; data block transfers regarding this upload should go to this URL.
         * This may also be an empty string, in which case you should keep using the normal URLs.
         */
        url: string;
    }
    /**
     * A single flight result within a larger download result.
     */
    export interface IUploadProcessingFlightStatus {
        /**
         * The flight record for the flight.
         */
        flightRecord: number;
        /**
         * A description of the flight record's processing state. This should be checked before assuming
         * that the flight is correct and complete.
         */
        flightStatus: ems.api.v1.transfer.UploadProcessingActivityState | string;
    }
    /**
     * Represents the status of an upload processing through EMS.
     */
    export interface IUploadProcessingStatus {
        /**
         * The upload's download record, if known.
         */
        downloadRecord?: number;
        /**
         * A description of the download record's processing state. This should be checked before assuming
         * that the download is correct and complete.
         */
        downloadState: ems.api.v1.transfer.UploadProcessingActivityState | string;
        /**
         * The upload's flight information, if known.
         */
        flights: ems.api.v1.transfer.IUploadProcessingFlightStatus[];
        /**
         * Any error message associated with the processing of the upload, if any.
         */
        errorMessage: string;
    }
    /**
     * Represents one record in the list of uploads that we know about on the server side.
     */
    export interface IUploadRecord {
        /**
         * The transfer ID of the transfer, set by the server when the transfer started.
         */
        id: string;
        /**
         * The type of the upload.
         */
        type: ems.api.v1.transfer.UploadType | string;
        /**
         * Alternate view onto the user's foreign key for simpler usage.
         */
        userId: number;
        /**
         * Alternate view onto the EMS system's foreign key for simpler usage.
         */
        emsSystemId: number;
        /**
         * Name field set by the client when the transfer started.
         */
        name: string;
        /**
         * The current number of bytes transferred.
         */
        currentCount: number;
        /**
         * The total size of the transfer, if known.
         */
        totalSize?: number;
        /**
         * The timestamp of the last time someone uploaded bytes to this transfer. This will
         * be used to determine when a transfer can be aborted automatically.
         */
        lastTransfer: any;
        /**
         * The timestamp of the last time someone tried to process this transfer. This will
         * be used to determine when a transfer can be aborted automatically.
         */
        lastProcessing: any;
        /**
         * The start time of the transfer, from when the initial start request is made.
         */
        startTime: any;
        /**
         * The finish time of the actual data transfer portion of the process.
         */
        transferFinishTime?: any;
        /**
         * The finish time of the processing portion of the process.
         */
        processingFinishTime?: any;
        /**
         * The state of the upload, up to and including EMS hand-off.
         */
        state: ems.api.v1.transfer.UploadState | string;
        /**
         * Metadata for the upload; this is somewhat arbitrary and specific to the type
         * of the transfer, but in any event, it will be a JSON blob with key/value pairs.
         */
        metadata: any;
    }
    /**
     * Encapsulates information that will be returned when beginning a new data ingestion transfer.
     */
    export interface IUploadRequest {
        /**
         * The name of the transfer; this is not necessarily unique, and is basically just
         * what the user passed to us when they initiated the transfer.
         */
        name: string;
        /**
         * The type of the upload; this is handler that we will use to process the uploaded data.
         * The format of the data will be determined by what this upload type is.
         */
        type: ems.api.v1.transfer.UploadType | string;
        /**
         * The password the data is encrypted with, if any. This point of the encryption possibility
         * is that this may be part of a store-and-forward system that stores the data encrypted locally.
         * (Thus passing it here along with the data is not particularly a security issue, but rather
         * a way to avoid having to decrypt it as it transfers.)
         */
        password: string;
        /**
         * The total size of the data to be transferred. This may be absent in the case where the upload
         * is being streamed and the total size is unknown.
         */
        totalSize?: number;
        /**
         * Arbitrary metadata attached to the transfer record. This may be used by post-processing later
         * or for other uses.
         */
        metadata: any;
    }
    /**
     * Summarizes the result of an upload operation.
     */
    export interface IUploadResult {
        /**
         * If this is true, the transfer was successful and also the transferred data are intact.
         * This includes things like verifying the integrity of zip archives, for the overall transfer
         * completion.
         */
        transferSuccessful: boolean;
        /**
         * If there was an error, then the error message will describe it. In the case of success, there
         * may also be a message here. This message will be suitable for display to a user.
         */
        message: string;
    }
    /**
     * This is sent as a response to an upload transfer status request.
     */
    export interface IUploadStatus {
        /**
         * The current number of bytes the server has received on this transfer.
         */
        currentCount: number;
        /**
         * The state of the upload.
         */
        state: ems.api.v1.transfer.UploadState | string;
        /**
         * Contains a user-readable message about the status of the transfer.
         */
        message: string;
    }
}
