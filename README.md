# guidgen-cli.net
.NET command line tool to generate GUID

## Usage
### Basic usage
```
$ guidgen
82cdb2a2-f79f-46ac-85f5-570313a6a7f8
```

### Output GUID in upper case
`-u, --upper`
```
$ guidgen -u
814FC2DF-6C28-4B1A-9891-46CCDB126369
```

### Output GUID according to the provided format specifier
`-f, --format [D/N/P/B/X]`
```
$ guidgen -f D
d50903b3-ed20-4006-8955-a6b63928399f

$ guidgen -f N
bf55bcb322da4df1820d656257101c05

$ guidgen -f P
(70e8db77-6ac7-4469-9384-34479ed52a5d)

$ guidgen -f B
{baf1ab52-5099-4732-934b-167b697d5e7e}

$ guidgen -f X
{0x745ba33d,0x0e30,0x4eee,{0x93,0x40,0x82,0xed,0xb7,0xf7,0xe6,0x29}}
```

### Number of GUIDs generate
`-n, --number <number>`
```
$ guidgen -n 5
ff9b889b-ca69-4685-8490-f05c6f76cc0f
c0a3025b-29d1-4089-921e-12cb6aa536a6
802cc20a-185a-4f63-b359-0ac8389611c7
3f6cf397-422e-4320-8fef-692dfd526d30
fc2e9f11-652b-42a3-a525-b7e274288ec6
```

### Copy to clipboard
`--clip`
```
$ guidgen | clip

$ guidgen --clip
657ef6eb-8396-4d51-8d7d-6191ba2b735e
```

## License
MIT
