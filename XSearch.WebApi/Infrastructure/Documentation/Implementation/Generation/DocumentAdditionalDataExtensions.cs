//  Copyright © Titian Software Ltd

using NSwag;

namespace XSearch.WebApi.Infrastructure.Documentation.Implementation.Generation
{
  internal static class DocumentAdditionalDataExtensions
  {
    public static void IncludeAdditionalData(this OpenApiDocument document, ReDocInfo reDocInfo)
    {
      document.Info.SetLogo();
      document.Tags.Organize(reDocInfo);
      document.Operations.FormatSecurityNotes();
    }

    private static void SetLogo(this NSwag.OpenApiInfo openApiInfo)
    {
      const string c_logoEmbeddedSource = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIwAAAAoCAYAAAAsTRLGAAAACXBIWXMAAA7CAAAOwgEVKEqAAAAIv0lEQVR42u2be3BU1R3HP78bNmx4JRQmqWhokGiFdaQ1gFKwlloqOgYoFYaHnY4tiFLGqcXadqa0zJRqmarYjn04jExpVapFiFIGqg7QEYhKeBoEihBCqEmgQEhCErLZ/fWPnKU3S+7N3WUjCb3fmZ29e8/r3vP7nt/rnBWSxK5dM59GZQHQ06Xa4ahaM0aOfGkXPq4KWMk0KimZORCVxzogC8ANIpGH/Wm+etAjmUZpadpHo+KJbIL0c62gKqwteRvkZtvd/ewrmMBiifoi6gaEqdn7j1+LcD8q0lb62gKy7GhkxfqUPUHRnkyQu+Lu5jB8RzZQ5YuoGxBGhB8AFqLtFc8HEiKM7q3q3dwr/ISI5tgGaZGovBwoPXXAF0P3N0ku5kbTEh0k3Kd5uqj8DGwKS0FF7wDu9MVwlTu9SQyT6VCQSTi9Aahry0nOk2HV+uK5fKjq06rapKqHVfWaK+L0phTTQ80UfRBCZej/+KVHKBzd4Is7JbjPRLP5wAigsnsTBmDK6Aqgwpdtp+AQ8HmgASi/EhpGM873Ot2Q0dQIZHQcV+sJFAdtIb4W6XzMBG4EzojI8U4hjMIKgVm08VIBaFHhzzeNW1G3e+esuQpzBQk6Mgv9V1qa9VwgEDkfDlsFAjm2whYRfbETbPb1QLWInO+gnpiJLBeRpquVLSLSAOzx182lBMhX1fe0FU2qukZVMxzqLlDV46Zuo6rOiSvvp6p/VNVK05fb54wZa1hcH+PN8zQl8rG1fzPBtp+o6kpVzbb10TOuTqGt7E5Vfde8f0d9X8yHiZMASngokM3A1cZpaiea0v2CzMrlyX23//XwrQILVQh5C82kBWVjS3p42QdTh53e9tGG+aIUgprn0RZFnh0buneTR7IIcMDYajteEpFvxdWdBBTFvbsC40Rku6oGgGKgIEHO1gKTRWSLGaccGJyERhDTfiNwdxJrpwIYLSJVqhoEGm1l3xCRIlX9OvAmHW/txHBORLJcfZhsBtwDTHJ5tZDCI6jOl9eOvArki1dt0ErVAiscCAKPi+pSoI+tbwTtCWzy2OVN7ZAF4AFVfUNEVhsh9AJWtrNQBLgf2A58tx2yHL0k9G+du2G2xdQP+DGwxfy2k6UB+DAJwR/yWHewzZ/MBZYZ38UJv4gjS60xW+pQv96D0yuf8SD6/mNXfTw8mib5SdlXmAw8DgTaKQ0k0FV8nuckEFPN04DV5noikOXQR+x9x9nuRYFpIrLGQbNlA8ttC2u8qvYVkXhyLRKRZxPUNHMTMMe9gc3AqFgoraqWQ92+wOg2xgTuEhFPea/LDqu1hwYuWpKEGxOgczAC2AkMAiaqakBEwsBUW52pwEJgbFxbm6ZjmxNZjFBPqupvbYRJB64FDsZV/YmqhoCwyzPXA28AW0VEjTn5NvDFDt41aoT+dxth+phnaQ/xmfrfeSVL18nDpB5NwCpDiH7AV1T1n8YfA6g2NnxeB/14CftPO9yvsWmzgcB3PPS1ELgX2AD8CFicwDufu4y58gyra8hXNsbdiKiy4TI7/YvtehLwNZvp+puIRDp5Qc0xxEwUMW1VmGC7zE9DUl1Cw3wpdM+U2jWjBwTE6mu/37j29rxgVvCEjN/SkkS0sVdVS4GbzeT3shWv6sgnN7hNVYMd5GkKHMZ/XVXXeBBktvE/BpnfMee1t63OTmCBizb4JvDTJKd/cIoII2edneaLdWpSQZgL60b+XIkuguglO+EX6uv+3VA0akyvKTsqktQyS4HPAbPNvXITNjvhYJw/876qrnQwT9cBD9p+NwMnVDUXvKUYjLOd5eCbxBAwBAqmwFLUm6godrBtkaqGTVrCbQFudCVMHYENfQm/BTrBIV/zcZToCylJuiHznY9NyLWWRSHw+yS6fgV4ykxoLIx8VUTcVsLrwA9tzuEtwDMex9skIvWq+gDwhySnI2Yq7Vr1FuCdlBh/kRZVXWsc6tii8BLBiSszQyxuHswv764jPRghPSP+k8uTN+bxq90pytO6J5AsTU9yck7Y8iJezBEiUgI8ksRwH8Zpm2Sx1XwnerAsnEDd+QnkuNo3SU3rRi61sQ5gSbCw5Plz5Y8urBX5Xrw2qubRPVszItOe2Hy2K7hB1YBd212wXT8FHI4pThGx76usB46Z62IbaZar6jvG9xmGS0bc+Dw7jOaK7V/tj3ser9huI/Q8QxovZ1iqzJj2o64xDWV/jjLzfg2qOsGE7F8Fru/gHduqGUOYeDVdXnrNjhvC2TXn20+sAciMhe+dPKRqJadplPLiGfl5TetG1rg6h8JjwftKnsPHFYers1Q34FiaM1kANMOfQp8wiUU4pJWB+1ECF81RanR6BzkRjfii6mqEEV40tv8CcMHrWZWd04eeQ1iCt6yoHce11b9A9OJeT3toJCJv+aLqGnB1dDaXlQWDaVmNLlUeHDO4/58ACl47ktmTyBBPo0atlu0Hh35k/6Na8/pRX4hE9ZJ8RDAQ2CcTi8/4ouoGhFFVq7ii5oQ4eerC+DG5/S+GrdWThuREreg0Ee3jEE6oJWzNWXN8W+ze/sOVIXqk5bbpVjWijVIcCmXX+yLqRoQBKD5WcysWU5Foj7iGpbdd1//lWBKsckpenkj0EM67pDbmyKLPFpUvKT1SNVOEVxxqlQ0fkpMv4v9dtiuhw72kMXlZu1R1NzXbR2Bp29T0WQlBq+MqRGd6IkurCpkDLBH0yy6cHXKgrDKXFJx09/EpEgaA2m1rEZ18ydaSpWjNuy9I1h0Pi5Cp3sc1voq4arjYcUUf3S2sbj0H61DmdozTx/9rHkZcSix/Gn3C+PDRBQljidsfrE5FG8584ouoOzq9nYThednLDxytqlDLym3rMmkkTfTtYaFQsy+i7kgYpRJx2maXpLWAOVe73hfDVadhZDZE5yFW2zxMNFqPZf0GICqUiPe4+n1/6rsnUpbn0IcKAtX/OfU8MBttc4A5TlmxVZXvDyo6vtOf/u6H/wKPuMgauhpO+QAAAABJRU5ErkJggg==";
      const string c_backgroundColor = "#444444";
      const string c_logoClickLink = "#"; // Will return to the top of the document

      var logoDetails = new Dictionary<string, object>
      {
        { "url", c_logoEmbeddedSource },
        { "backgroundColor", c_backgroundColor },
        { "href", c_logoClickLink }
      };

      openApiInfo.ExtensionData ??= new Dictionary<string, object>();
      openApiInfo.ExtensionData.Add("x-logo", logoDetails);
    }

    private static void Organize(this ICollection<OpenApiTag> tags, ReDocInfo reDocInfo)
    {
      tags.Clear();
      tags.AddTagsWithDescriptions(reDocInfo.TagsOrder, reDocInfo.DocumentationSections);
    }

    private static void AddTagsWithDescriptions(
      this ICollection<OpenApiTag> tags,
      IEnumerable<string> tagsOrder,
      IReadOnlyCollection<DocumentationSection> sections)
    {
      foreach (var tag in tagsOrder)
      {
        var openApiTag = new OpenApiTag
        {
          Name = tag,
          Description = sections
            .SingleOrDefault(section => section.Title == tag)?.Content
        };

        tags.Add(openApiTag);
      }
    }

    private static void FormatSecurityNotes(this IEnumerable<OpenApiOperationDescription> operations)
    {
      foreach (var operation in operations)
      {
        var securityNoteExists = operation.Operation.ExtensionData
          .TryGetValue(ExtensionDataKeys.SecurityNote, out var securityNote);

        if (securityNoteExists)
        {
          operation.Operation.Description += $"\n\n### Authorisation:\n{securityNote}";
        }
      }
    }
  }
}
