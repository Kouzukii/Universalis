﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Universalis.Application.Common;

namespace Universalis.Application.Views.V1;
/*
 * Note for anyone viewing this file: People rely on the field order (even though JSON is defined to be unordered).
 * Please do not edit the field order unless it is unavoidable.
 */

public class CurrentlyShownMultiView : PartiallySerializable
{
    /// <summary>
    /// The item IDs that were requested.
    /// </summary>
    [JsonPropertyName("itemIDs")]
    public List<int> ItemIds { get; init; } = new();

    /// <summary>
    /// The item data that was requested, as a list. Use the nested item IDs
    /// to pull the item you want, or consider using the v2 endpoint instead.
    /// </summary>
    [JsonPropertyName("items")]
    public List<CurrentlyShownView> Items { get; init; } = new();

    /// <summary>
    /// The ID of the world requested, if applicable.
    /// </summary>
    [JsonPropertyName("worldID")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? WorldId { get; init; }

    /// <summary>
    /// The name of the DC requested, if applicable.
    /// </summary>
    [JsonPropertyName("dcName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string DcName { get; init; }
    
    /// <summary>
    /// The name of the region requested, if applicable.
    /// </summary>
    [JsonPropertyName("regionName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RegionName { get; init; }

    /// <summary>
    /// A list of IDs that could not be resolved to any item data.
    /// </summary>
    [JsonPropertyName("unresolvedItems")]
    public int[] UnresolvedItemIds { get; init; }

    /// <summary>
    /// The name of the world requested, if applicable.
    /// </summary>
    [JsonPropertyName("worldName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string WorldName { get; init; }
}